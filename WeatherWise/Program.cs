using System;
using System.Diagnostics;
using System.Net;
using System.Net.Mail;
using RazorEngine;
using RazorEngine.Templating;
using System.Net.Mime;
using System.IO;
using WeatherWise.Models;

namespace WeatherWise
{
    class Program
    {
        private static string CurrentDirectory = System.IO.Directory.GetCurrentDirectory();
        private static string ApiUrl;
        private static Config AppConfig;
        static void Main(string[] args)
        {
            //https://darksky.net/dev/docs#forecast-request
            var configFile = System.IO.Directory.GetCurrentDirectory() + "\\Config.json";
            if (!File.Exists(configFile))
            {
                File.WriteAllText(configFile, Newtonsoft.Json.JsonConvert.SerializeObject(new Config(), Newtonsoft.Json.Formatting.Indented));
                Console.WriteLine($"No config file has been set, a config file has been created at {configFile}. Please edit with your own information to continue");
                return;
            }

            AppConfig = Newtonsoft.Json.JsonConvert.DeserializeObject<Config>(System.IO.File.ReadAllText(configFile));

            if (AppConfig.ApiKey == null)
            {
                Console.WriteLine("Missing API KEY in Config.json file");
                return;
            }
            ApiUrl = string.Format("http://api.wunderground.com/api/{0}/", AppConfig.ApiKey);

            //$"{ApiUrl}conditions/q/{state}/{city}.json"
            var forecastJson = new System.Net.WebClient().DownloadString($"{ApiUrl}forecast/q/{AppConfig.State}/{AppConfig.City}.json");
            var currentConditionsJson = new System.Net.WebClient().DownloadString($"{ApiUrl}conditions/q/{AppConfig.State}/{AppConfig.City}.json");
            //"pressure_in
            var current = Newtonsoft.Json.JsonConvert.DeserializeObject<ConditionsResponse>(currentConditionsJson);
            var forecast = Newtonsoft.Json.JsonConvert.DeserializeObject<ForecastResponse>(forecastJson);
            var todaysForecast = forecast.forecast.simpleforecast.ForecastDay[0];


            //MockEmail(GenerateEmailTemplate(today), AppConfig.ToEmailAddress);
            var model = GetModelFromSimpleDayForecast(todaysForecast, current.current_observation.pressure_in);
            //var model = GetModelFromCurrentConditions(current.current_observation);
            var template = GenerateEmailTemplate(model);
            SendEmail(template, AppConfig.ToEmailAddress, todaysForecast);
        }

        public static EmailModel GetModelFromCurrentConditions(ConditionsResponse.Current_Observation current)
        {
            return new EmailModel()
            {
                AverageHumidity = current.relative_humidity,
                BarometricPressure = current.pressure_in,
                High = current.temp_f,
                Low = current.temp_f,
                PreciptationPercent = 0f//current.precip_1hr_in
            };
        }

        public static EmailModel GetModelFromSimpleDayForecast(ForecastResponse.SimpleForecastDay forecast, string pressure)
        {
            return new EmailModel()
            {
                AverageHumidity = forecast.avehumidity.ToString(),
                BarometricPressure = pressure,
                High = forecast.high.fahrenheit,
                Low = forecast.low.fahrenheit,
                PreciptationPercent = forecast.pop
            };
        }

        public static string GenerateEmailTemplate(EmailModel model)
        {            
            var result =
                Engine.Razor.RunCompile(System.IO.File.ReadAllText(CurrentDirectory + "\\EmailTemplate.cshtml"), "templateKey", null, model);
            return result;
        }

        public static void MockEmail(string message, string email)
        {
            var fileName = System.IO.Directory.GetCurrentDirectory() + "\\Email.html";
            System.IO.File.WriteAllText(fileName, message);
            Process.Start(fileName);
        }

        public static void SendEmail(string message, string email, ForecastResponse.SimpleForecastDay forecast)
        {

            var smtpClient = new SmtpClient(AppConfig.SmtpServer)
            {
                Port = 587,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                EnableSsl = true,
                Credentials = new NetworkCredential(AppConfig.SmtpUser, AppConfig.SmtpPassword)
            };
            MailMessage msg = new MailMessage();
            msg.From = new MailAddress(AppConfig.ToEmailAddress);
            msg.To.Add(new MailAddress(AppConfig.ToEmailAddress));

            msg.Subject = "Weather Report";
            msg.IsBodyHtml = true;

            //var contentId = AttachImage(msg, @"\images\cardigan.png");

            //msg.Body = $@"<img width=""100"" src=""cid:{contentId}"" alt=""missing images"" />";// message;
            msg.Body = message;
            Console.WriteLine("Sending email");
            smtpClient.Send(msg);
            Console.WriteLine("Sent");
        }

        public static string AttachImage(MailMessage msg, string path)
        {
            string attachmentPath = Environment.CurrentDirectory + path;
            Attachment inline = new Attachment(attachmentPath);
            inline.ContentDisposition.Inline = true;
            inline.ContentDisposition.DispositionType = DispositionTypeNames.Inline;
            inline.ContentId = Guid.NewGuid().ToString();
            inline.ContentType.MediaType = "image/png";
            inline.ContentType.Name = Path.GetFileName(attachmentPath);

            msg.Attachments.Add(inline);
            return inline.ContentId;
        }

    }
}
