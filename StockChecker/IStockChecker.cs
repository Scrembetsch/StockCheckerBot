using System.Net.Http;
using System.Threading.Tasks;

namespace StockCheckerBot.WebsiteChecker
{
    public interface IStockChecker
    {
        public enum CheckState
        {
            InStock,
            NotAvailable,
            RequestError,

            NumStates
        }

        public void RegisterUrl(string checkUrl, string openUrl, string alias);
        public void UnRegisterUrl(string checkUrl);
        public string GetFallBackUrl();

        public void SetCheckInterval(float checkEverySeconds);
        public bool Check(HttpResponseMessage response);

        public Task<bool> Run();

        public void SetSound(string path, CheckState state);
    }
}