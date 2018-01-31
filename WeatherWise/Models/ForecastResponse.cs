using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherWise.Models
{
    public class ForecastResponse
    {
        public Response response { get; set; }
        public Forecast forecast { get; set; }

        public class Response
        {
            public string version { get; set; }
            public string termsofService { get; set; }
            public Features features { get; set; }
        }

        public class Features
        {
            public float? forecast { get; set; }
        }

        public class Forecast
        {
            public Txt_Forecast txt_forecast { get; set; }
            public Simpleforecast simpleforecast { get; set; }
        }

        public class Txt_Forecast
        {
            public string date { get; set; }
            public Forecastday[] forecastday { get; set; }
        }

        public class Forecastday
        {
            public float? period { get; set; }
            public string icon { get; set; }
            public string icon_url { get; set; }
            public string title { get; set; }
            public string fcttext { get; set; }
            public string fcttext_metric { get; set; }
            public string pop { get; set; }
        }

        public class Simpleforecast
        {
            [Newtonsoft.Json.JsonProperty("forecastday")]
            public SimpleForecastDay[] ForecastDay { get; set; }
        }

        public class SimpleForecastDay
        {
            [Newtonsoft.Json.JsonIgnore]
            public UpperTorsoAttires UpperTorso
            {
                get
                {
                    if (high.fahrenheit <= 60)
                        return UpperTorsoAttires.Sweater;
                    if (high.fahrenheit > 60 && high.fahrenheit <= 70)
                        return UpperTorsoAttires.LongSleeve;
                    return UpperTorsoAttires.ShortSleeve;
                }
            }
            [Newtonsoft.Json.JsonIgnore]
            public LowerTorsoAttires LowerTorso
            {
                get
                {
                    if (high.fahrenheit <= 70)
                        return LowerTorsoAttires.Pants;
                    return LowerTorsoAttires.Shorts;
                }
            }
            [Newtonsoft.Json.JsonIgnore]
            public AccessoryAttires[] Accessories
            {
                get
                {
                    var accessories = new List<AccessoryAttires>();

                    if (high.fahrenheit < 50)
                    {
                        accessories.Add(AccessoryAttires.Gloves);
                        accessories.Add(AccessoryAttires.Scarf);
                    }

                    return accessories.ToArray();                    
                }
            }
            [Newtonsoft.Json.JsonIgnore]
            public FootwearAttires Footwear {
                get
                {
                    if (high.fahrenheit > 80)
                        return FootwearAttires.FlipFlops;
                    return FootwearAttires.Shoes;
                }
            }
            [Newtonsoft.Json.JsonIgnore]
            public OuterwearAttires Outerwear
            {
                get
                {
                    if (high.fahrenheit < 70 && high.fahrenheit > 60)
                        return OuterwearAttires.Jacket;
                    if (high.fahrenheit <= 60)
                        return OuterwearAttires.Coat;
                    return OuterwearAttires.None;
                }
            }
            [Newtonsoft.Json.JsonIgnore]
            public HeadwearAttires Headwear
            {
                get
                {
                    if (high.fahrenheit > 55 && high.fahrenheit < 75)
                        return HeadwearAttires.Cap;
                    if (high.fahrenheit <= 55)
                        return HeadwearAttires.StockingHat;
                    return HeadwearAttires.None;
                }
            }
            [Newtonsoft.Json.JsonIgnore]
            public bool IsUmbrellaNeeded
            {
                get
                {
                    return (pop > 49);
                }
            }
            public enum HeadwearAttires
            {
                StockingHat,
                Cap,
                None
            }
            public enum OuterwearAttires
            {
                Jacket,
                Coat,
                None
            }

            public enum UpperTorsoAttires
            {
                ShortSleeve,
                LongSleeve,
                Sweater
            }
            public enum LowerTorsoAttires
            {
                Shorts,
                Pants,
                LongJohns
            }
            public enum AccessoryAttires
            {
                Hat,
                Gloves,
                Scarf
            }
            public enum FootwearAttires
            {
                FlipFlops,
                Shoes
            }

            public Date date { get; set; }
            public float? period { get; set; }
            public High high { get; set; }
            public Low low { get; set; }
            public string conditions { get; set; }
            public string icon { get; set; }
            public string icon_url { get; set; }
            public string skyicon { get; set; }
            public float? pop { get; set; }
            public Qpf_Allday qpf_allday { get; set; }
            public Qpf_Day qpf_day { get; set; }
            public Qpf_Night qpf_night { get; set; }
            public Snow_Allday snow_allday { get; set; }
            public Snow_Day snow_day { get; set; }
            public Snow_Night snow_night { get; set; }
            public Maxwind maxwind { get; set; }
            public Avewind avewind { get; set; }
            public float? avehumidity { get; set; }
            public float? maxhumidity { get; set; }
            public float? minhumidity { get; set; }
        }

        public class Date
        {
            public string epoch { get; set; }
            public string pretty { get; set; }
            public float? day { get; set; }
            public float? month { get; set; }
            public float? year { get; set; }
            public float? yday { get; set; }
            public float? hour { get; set; }
            public string min { get; set; }
            public float? sec { get; set; }
            public string isdst { get; set; }
            public string monthname { get; set; }
            public string monthname_short { get; set; }
            public string weekday_short { get; set; }
            public string weekday { get; set; }
            public string ampm { get; set; }
            public string tz_short { get; set; }
            public string tz_long { get; set; }
        }

        public class High
        {
            public float? fahrenheit { get; set; }
            public float? celsius { get; set; }
        }

        public class Low
        {
            public float? fahrenheit { get; set; }
            public float? celsius { get; set; }
        }

        public class Qpf_Allday
        {
            [Newtonsoft.Json.JsonProperty("in")]
            public float? _in { get; set; }
            public float? mm { get; set; }
        }

        public class Qpf_Day
        {
            [Newtonsoft.Json.JsonProperty("in")]
            public float? _in { get; set; }
            public float? mm { get; set; }
        }

        public class Qpf_Night
        {
            [Newtonsoft.Json.JsonProperty("in")]
            public float? _in { get; set; }
            public float? mm { get; set; }
        }

        public class Snow_Allday
        {
            [Newtonsoft.Json.JsonProperty("in")]
            public float? _in { get; set; }
            public float? cm { get; set; }
        }

        public class Snow_Day
        {
            [Newtonsoft.Json.JsonProperty("in")]
            public float? _in { get; set; }
            public float? cm { get; set; }
        }

        public class Snow_Night
        {
            [Newtonsoft.Json.JsonProperty("in")]
            public float? _in { get; set; }
            public float? cm { get; set; }
        }

        public class Maxwind
        {
            public float? mph { get; set; }
            public float? kph { get; set; }
            public string dir { get; set; }
            public float? degrees { get; set; }
        }

        public class Avewind
        {
            public float? mph { get; set; }
            public float? kph { get; set; }
            public string dir { get; set; }
            public float? degrees { get; set; }
        }

    }
}
