using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace StockCheckerBot.StockChecker
{
    public interface IStockChecker
    {
        public enum CheckState
        {
            InStock,
            NotAvailable,
            Error,

            NumStates
        }
        public void RegisterUrl(string checkUrl, string openUrl, string alias);
        public void UnRegisterUrl(string checkUrl);

        public void SetCheckInterval(float checkEverySeconds);

        public Task<bool> Run();
        public bool Check(HttpResponseMessage response);

        public void SetSound(string path, CheckState state);
    }
}
