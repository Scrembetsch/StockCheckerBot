using StockCheckerBot.Config.Element;
using StockCheckerBot.Config.Section;
using System.Configuration;
using System.Net.Http;

namespace StockCheckerBot.WebsiteChecker
{
    public class AmdChecker : BaseStockChecker
    {
        private static readonly string s_ConfigSection = "AmdChecker";
        private readonly string _CheckUrlBase;
        private readonly string _OpenUrlBase;
        private readonly string _ContainString;

        public AmdChecker() : base(s_ConfigSection)
        {
            AmdCheckerConfigSection config = (AmdCheckerConfigSection)ConfigurationManager.GetSection(s_ConfigSection);

            _CheckUrlBase = config.Websites.BaseCheckUrl;
            _OpenUrlBase = config.Websites.BaseOpenUrl;

            _ContainString = config.ContainString;

            foreach (ItemConfigElement item in config.Items)
            {
                AddProduct(item.ProductId, item.Alias);
            }
        }

        public void AddProduct(string productId, string alias)
        {
            string checkUrl = _CheckUrlBase.Replace("{ProductId}", productId);
            string openUrl = _OpenUrlBase.Replace("{ProductId}", productId);
            RegisterUrl(checkUrl, openUrl, alias);
        }

        public override bool Check(HttpResponseMessage response)
        {
            return response.Content.ReadAsStringAsync().Result.Contains(_ContainString);
        }
    }
}