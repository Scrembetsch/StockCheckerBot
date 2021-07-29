using StockCheckerBot.WebsiteChecker;

namespace StockCheckerBot
{
    internal static class Program
    {
        private static void Main(string[] args)
        {
            _ = new AmdChecker().Run();
        }
    }
}