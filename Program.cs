using StockCheckerBot.StockChecker;
using System;
using System.Threading.Tasks;

namespace StockCheckerBot
{
    class Program
    {
        static void Main(string[] args)
        {
            AmdStockChecker amdChecker = new AmdStockChecker();
            amdChecker.AddProduct("5458374100", "RX 6800XT");
            amdChecker.AddProduct("5496921500", "RX 6800XT Midnight");
            amdChecker.AddProduct("5458374200", "RX 6900XT");
            ////amdChecker.AddProduct("5450881600", "R7 5800X");

            //amdChecker.SetCheckInterval(30.0f);

            //amdChecker.SetSound("Data/Giorno.wav", IWebsiteChecker.CheckState.InStock);
            ////amdChecker.SetSound("!Hand", IWebsiteChecker.CheckState.Error);
            Task<bool> temp = amdChecker.Run();
            //Console.ReadLine();
        }
    }
}
