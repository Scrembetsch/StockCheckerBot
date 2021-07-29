using StockCheckerBot.Config.Element;
using System.Configuration;

namespace StockCheckerBot.Config.Section
{
    public class AmdCheckerConfigSection : BaseCheckerConfigSection
    {
        private const string WebsitesId = "Websites";
        private const string ContainStringId = "ContainString";
        private const string ItemsId = "Items";

        [ConfigurationProperty(WebsitesId)]
        public WebsiteConfigElement Websites
        {
            get
            {
                return (WebsiteConfigElement)this[WebsitesId];
            }
            set
            {
                this[WebsitesId] = value;
            }
        }

        [ConfigurationProperty(ContainStringId)]
        public string ContainString
        {
            get
            {
                return (string)this[ContainStringId];
            }
            set
            {
                this[ContainStringId] = value;
            }
        }

        [ConfigurationProperty(ItemsId)]
        [ConfigurationCollection(typeof(ItemConfigElementCollection), AddItemName = "Add")]
        public ItemConfigElementCollection Items
        {
            get
            {
                return (ItemConfigElementCollection)this[ItemsId];
            }
            set
            {
                this[ItemsId] = value;
            }
        }
    }
}