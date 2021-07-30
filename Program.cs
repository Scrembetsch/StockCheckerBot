using StockCheckerBot.Util;
using StockCheckerBot.WebsiteChecker;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Threading.Tasks;

namespace StockCheckerBot
{
    internal static class Program
    {
        private static List<IStockChecker> _Checkers = new List<IStockChecker>();
        private static List<Task<bool>> _Tasks;
        private static void Main()
        {
            _Checkers.Add(new AmdChecker());
            do
            {
                try
                {
                    RunCheckers();
                    foreach(var task in _Tasks)
                    {
                        task.Wait();
                    }
                }
                catch (Exception)
                {
                    OpenFallbackPages();
                }
            } while (ConfigurationManager.AppSettings["RestartOnCrash"] == "true");
        }

        private static void RunCheckers()
        {
            _Tasks = new List<Task<bool>>();
            foreach(var checker in _Checkers)
            {
                _Tasks.Add(checker.Run());
            }
        }

        private static void OpenFallbackPages()
        {
            foreach (var checker in _Checkers)
            {
                string url = checker.GetFallBackUrl();
                if (!string.IsNullOrWhiteSpace(url))
                {
                    Web.OpenUrl(url);
                }
            }
        }
    }
}