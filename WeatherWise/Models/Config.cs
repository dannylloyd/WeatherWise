using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherWise.Models
{
    public class Config
    {
        public string ApiKey { get; set; }
        public string ToEmailAddress { get; set; }
        public string SmtpServer { get; set; }
        public string SmtpUser { get; set; }
        public string SmtpPassword { get; set; }
        public string City { get; set; }
        public string State { get; set; }

        public Config()
        {

        }

        public Config(bool example)
        {
            if (example)
            {
                ApiKey = "WeatherUndergroundAPIKey";
                ToEmailAddress = "Email address you want to send to";
                SmtpServer = "Like smtp.gmail.com";
                SmtpUser = "carl@gmail.com";
                SmtpPassword = "carlspassword";
                City = "City to check weather";
                State = "State in which city exists";
            }
        }
    }

}
