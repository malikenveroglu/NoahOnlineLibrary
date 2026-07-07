using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoahOnlineLibrary.ConsoleApp.UI
{
    internal static class MainMenuUI
    {
        public static void Draw()
        {
            BookFrame.Draw();

            Console.ForegroundColor = ConsoleColor.Green;

            Console.SetCursorPosition(82, 2);
            Console.Write("NOAH ONLINE LIBRARY");

            Console.SetCursorPosition(85, 4);
            Console.Write("--- PA 302 ---");

            Console.SetCursorPosition(68, 7);
            Console.Write("1. Book Management");

            Console.SetCursorPosition(68, 9);
            Console.Write("2. Author Management");

            Console.SetCursorPosition(68, 11);
            Console.Write("3. Reservation Management");

            Console.SetCursorPosition(68, 13);
            Console.Write("0. Exit");

            Console.SetCursorPosition(68, 19);
            Console.Write("Select Option : ");

            Console.ResetColor();
        }
    }
}
