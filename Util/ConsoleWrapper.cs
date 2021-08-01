using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockCheckerBot.Util
{
    public static class ConsoleWrapper
    {
        public static void WriteLine(object value, ConsoleColor foregroundColor = ConsoleColor.White)
        {
            ConsoleColor temp = Console.ForegroundColor;
            Console.ForegroundColor = foregroundColor;
            Console.WriteLine(value);
            Console.ForegroundColor = temp;
        }
    }
}
