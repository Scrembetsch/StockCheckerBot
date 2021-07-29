using System.Configuration;

namespace StockCheckerBot.Config.Element
{
    public class WebsiteConfigElement : ConfigurationElement
    {
        private const string BaseOpenUrlId = "BaseOpenUrl";
        private const string BaseCheckUrlId = "BaseCheckUrl";

        [ConfigurationProperty(BaseOpenUrlId, IsRequired = true)]
        public string BaseOpenUrl
        {
            get
            {
                return (string)this[BaseOpenUrlId];
            }
            set
            {
                this[BaseOpenUrlId] = value;
            }
        }

        [ConfigurationProperty(BaseCheckUrlId, IsRequired = true)]
        public string BaseCheckUrl
        {
            get
            {
                return (string)this[BaseCheckUrlId];
            }
            set
            {
                this[BaseCheckUrlId] = value;
            }
        }
    }
}