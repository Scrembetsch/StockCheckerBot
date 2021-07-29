using System.Configuration;

namespace StockCheckerBot.Config.Element
{
    public class SoundConfigElement : ConfigurationElement
    {
        private const string InStockId = "InStock";
        private const string NotAvailableId = "NotAvailable";
        private const string RequestErrorId = "RequestError";

        [ConfigurationProperty(InStockId)]
        public string InStock
        {
            get
            {
                return (string)this[InStockId];
            }
            set
            {
                this[InStockId] = value;
            }
        }

        [ConfigurationProperty(NotAvailableId)]
        public string NotAvailable
        {
            get
            {
                return (string)this[NotAvailableId];
            }
            set
            {
                this[NotAvailableId] = value;
            }
        }

        [ConfigurationProperty(RequestErrorId)]
        public string RequestError
        {
            get
            {
                return (string)this[RequestErrorId];
            }
            set
            {
                this[RequestErrorId] = value;
            }
        }
    }
}