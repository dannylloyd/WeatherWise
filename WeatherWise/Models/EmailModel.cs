using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherWise.Models
{
    public class EmailModel
    {
        public float? High { get; set; }
        public float? Low { get; set; }
        public string AverageHumidity { get; set; }
        public string BarometricPressure { get; set; }
        public float? PreciptationPercent { get; set; }

        [Newtonsoft.Json.JsonIgnore]
        public UpperTorsoAttires UpperTorso
        {
            get
            {
                if (High <= 60)
                    return UpperTorsoAttires.Sweater;
                if (High > 60 && High <= 70)
                    return UpperTorsoAttires.LongSleeve;
                return UpperTorsoAttires.ShortSleeve;
            }
        }
        [Newtonsoft.Json.JsonIgnore]
        public LowerTorsoAttires LowerTorso
        {
            get
            {
                if (High <= 70)
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

                if (High < 50)
                {
                    accessories.Add(AccessoryAttires.Gloves);
                    accessories.Add(AccessoryAttires.Scarf);
                }

                return accessories.ToArray();
            }
        }
        [Newtonsoft.Json.JsonIgnore]
        public FootwearAttires Footwear
        {
            get
            {
                if (High > 80)
                    return FootwearAttires.FlipFlops;
                return FootwearAttires.Shoes;
            }
        }
        [Newtonsoft.Json.JsonIgnore]
        public OuterwearAttires Outerwear
        {
            get
            {
                if (High < 70 && High > 60)
                    return OuterwearAttires.Jacket;
                if (High <= 60)
                    return OuterwearAttires.Coat;
                return OuterwearAttires.None;
            }
        }
        [Newtonsoft.Json.JsonIgnore]
        public HeadwearAttires Headwear
        {
            get
            {
                if (High > 55 && High < 75)
                    return HeadwearAttires.Cap;
                if (High <= 55)
                    return HeadwearAttires.StockingHat;
                return HeadwearAttires.None;
            }
        }
        [Newtonsoft.Json.JsonIgnore]
        public bool IsUmbrellaNeeded
        {
            get
            {
                return (PreciptationPercent > 49);
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
    }
}
