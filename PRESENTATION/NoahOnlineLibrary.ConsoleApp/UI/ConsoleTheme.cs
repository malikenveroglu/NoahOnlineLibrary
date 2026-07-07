using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoahOnlineLibrary.ConsoleApp.UI
{
    internal static class ConsoleTheme
    {
        public static void Apply()
        {
            Console.Title = "NOAH ONLINE LIBRARY";

            Console.OutputEncoding = System.Text.Encoding.UTF8;

            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.Green;

            Console.Clear();
        }
    }
}
