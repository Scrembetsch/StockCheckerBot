using StockCheckerBot.Util;
using StockCheckerBot.WebsiteChecker;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Threading;
using System.Threading.Tasks;

namespace StockCheckerBot
{
    internal static class Program
    {
        private readonly static List<IStockChecker> _Checkers = new List<IStockChecker>();
        private static List<Task<bool>> _Tasks;

        private static DateTime LastRestartTime = DateTime.MinValue;
        private static void Main()
        {
            _Checkers.Add(new AmdChecker());
            do
            {
                double timeDifference = GetRestartThreshold() - (DateTime.Now - LastRestartTime).TotalSeconds;
                if(timeDifference > 0.0)
                {
                    Thread.Sleep((int)(timeDifference * 1000));
                }
                LastRestartTime = DateTime.Now;

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
            } while (ShouldRestart());
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

        private static int GetRestartThreshold()
        {
            return int.Parse(ConfigurationManager.AppSettings["RestartThreshold"]);
        }

        private static bool ShouldRestart()
        {
            return ConfigurationManager.AppSettings["RestartOnCrash"] == "true";
        }
    }
}