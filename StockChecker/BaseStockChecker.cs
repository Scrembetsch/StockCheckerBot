using StockCheckerBot.Config.Section;
using StockCheckerBot.Util;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Media;
using System.Net.Http;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;

namespace StockCheckerBot.WebsiteChecker
{
    public class BaseStockChecker : IStockChecker
    {
        private readonly List<string> _CheckUrls = new List<string>();
        private readonly List<string> _OpenUrls = new List<string>();
        private readonly List<string> _Alias = new List<string>();
        private readonly List<string> _SoundFiles = new List<string>();
        private string _FallbackUrl;

        private float _CheckInterval = 30.0f;

        public BaseStockChecker(string configSection)
        {
            BaseCheckerConfigSection config = (BaseCheckerConfigSection)ConfigurationManager.GetSection(configSection);

            for (int i = 0; i < (int)IStockChecker.CheckState.NumStates; i++)
            {
                _SoundFiles.Add("");
            }

            _CheckInterval = config.CheckInterval;
            _FallbackUrl = config.FallbackUrl;

            SetSound(config.Sounds.InStock, IStockChecker.CheckState.InStock);
            SetSound(config.Sounds.NotAvailable, IStockChecker.CheckState.NotAvailable);
            SetSound(config.Sounds.RequestError, IStockChecker.CheckState.RequestError);
        }

        public virtual bool Check(HttpResponseMessage response)
        {
            return false;
        }

        public void RegisterUrl(string checkUrl, string openUrl, string alias)
        {
            _CheckUrls.Add(checkUrl);
            _OpenUrls.Add(openUrl);
            _Alias.Add(alias);
        }

        public async virtual Task<bool> Run()
        {
            HttpClient client = Web.CreateClient();

            // Do until user cancels
            while (true)
            {
                DateTime begin = DateTime.Now;
                Console.WriteLine(begin);
                // Send all requests to get product info
                var responseTasks = SendAllRequests(client);

                CheckAllRequests(responseTasks);

                DateTime end = DateTime.Now;
                float msSinceStart = (float)(end - begin).TotalMilliseconds;
                int waitTime = (int)(_CheckInterval * 1000.0f - msSinceStart);
                waitTime = Math.Max(0, waitTime);
                Thread.Sleep(waitTime);
            }
        }

        public void SetCheckInterval(float checkEverySeconds)
        {
            _CheckInterval = checkEverySeconds;
        }

        public void UnRegisterUrl(string checkUrl)
        {
            while (true)
            {
                int index = FindUrl(checkUrl);
                if (index == -1)
                {
                    return;
                }

                _Alias.RemoveAt(index);
                _OpenUrls.RemoveAt(index);
                _CheckUrls.RemoveAt(index);
            }
        }

        private int FindUrl(string url)
        {
            for (int i = 0; i < _CheckUrls.Count; i++)
            {
                if (_CheckUrls[i] == url)
                {
                    return i;
                }
            }
            return -1;
        }

        private List<Task<HttpResponseMessage>> SendAllRequests(HttpClient client)
        {
            List<Task<HttpResponseMessage>> responses = new List<Task<HttpResponseMessage>>();
            foreach (string url in _CheckUrls)
            {
                responses.Add(client.GetAsync(url));
            }
            return responses;
        }

        private void CheckAllRequests(List<Task<HttpResponseMessage>> responseTasks)
        {
            // Do until all responses are checked
            while (responseTasks.Count > 0)
            {
                for (int i = 0; i < responseTasks.Count; i++)
                {
                    if (responseTasks[i].IsCompleted)
                    {
                        CheckResponse(responseTasks[i].Result);
                        responseTasks.RemoveAt(i--);
                    }
                }
                // Wait small amount of time to save computation time
                Thread.Sleep(50);
            }
        }

        private void CheckResponse(HttpResponseMessage response)
        {
            int index = FindUrl(response.RequestMessage.RequestUri.ToString());
            if (response.IsSuccessStatusCode)
            {
                if (Check(response))
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine($"{_Alias[index]}: SUCCESS");
                    Web.OpenUrl(_OpenUrls[index]);
                    PlaySound(IStockChecker.CheckState.InStock);
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.WriteLine($"{_Alias[index]}: Not available");
                    PlaySound(IStockChecker.CheckState.NotAvailable);
                }
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"{_Alias[index]}: ERROR");
                PlaySound(IStockChecker.CheckState.RequestError);
            }
        }

        public void SetSound(string path, IStockChecker.CheckState state)
        {
            if (!string.IsNullOrWhiteSpace(path) && !path.StartsWith("!"))
            {
                path = $"{Environment.CurrentDirectory}/{path}";
            }
            _SoundFiles[(int)state] = path;
        }

        public void PlaySound(IStockChecker.CheckState state)
        {
            Sound.PlaySound(_SoundFiles[(int)state]);
        }

        public string GetFallBackUrl()
        {
            return _FallbackUrl;
        }
    }
}