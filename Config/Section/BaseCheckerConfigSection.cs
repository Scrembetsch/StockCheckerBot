using StockCheckerBot.Config.Element;
using System.Configuration;

namespace StockCheckerBot.Config.Section
{
    public class BaseCheckerConfigSection : ConfigurationSection
    {
        private const string CheckIntervalId = "CheckInterval";
        private const string SiteOpenThreshholdId = "SiteOpenThreshold";
        private const string FallbackUrlId = "FallbackUrl";
        private const string SoundsId = "Sounds";

        [ConfigurationProperty(CheckIntervalId, DefaultValue = 30)]
        public int CheckInterval
        {
            get
            {
                return (int)this[CheckIntervalId];
            }
            set
            {
                this[CheckIntervalId] = value;
            }
        }

        [ConfigurationProperty(SiteOpenThreshholdId, DefaultValue = 30)]
        public int SiteOpenThreshold
        {
            get
            {
                return (int)this[SiteOpenThreshholdId];
            }
            set
            {
                this[SiteOpenThreshholdId] = value;
            }
        }

        [ConfigurationProperty(FallbackUrlId, DefaultValue = "")]
        public string FallbackUrl
        {
            get
            {
                return (string)this[FallbackUrlId];
            }
            set
            {
                this[FallbackUrlId] = value;
            }
        }

        [ConfigurationProperty(SoundsId)]
        public SoundConfigElement Sounds
        {
            get
            {
                return (SoundConfigElement)this[SoundsId];
            }
            set
            {
                this[SoundsId] = value;
            }
        }
    }
}