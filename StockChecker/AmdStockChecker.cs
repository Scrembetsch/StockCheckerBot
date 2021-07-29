using StockCheckerBot.Config;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace StockCheckerBot.StockChecker
{
    public class AmdStockChecker : BaseStockChecker
    {
        private static readonly string s_ConfigSection = "AmdCheckerSection";
        private string _CheckUrlBase;
        private string _OpenUrlBase;

        public AmdStockChecker() : base(s_ConfigSection)
        {
            AmdCheckerConfigSection config = (AmdCheckerConfigSection)ConfigurationManager.GetSection(s_ConfigSection);

            _CheckUrlBase = config.CheckBaseUrl;
            _OpenUrlBase = config.OpenBaseUrl;
            //FallbackUrl = config.Websites.FallbackPage;
        }

        public void AddProduct(string productId, string alias)
        {
            string checkUrl = _CheckUrlBase.Replace("{productId}", productId);
            string openUrl = _OpenUrlBase.Replace("{productId}", productId);
            RegisterUrl(checkUrl, openUrl, alias);
        }
        public override bool Check(HttpResponseMessage response)
        {
            return response.Content.ReadAsStringAsync().Result.Contains("Add to cart");
        }
    }
}
