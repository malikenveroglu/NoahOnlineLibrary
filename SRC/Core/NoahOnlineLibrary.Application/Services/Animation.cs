using NoahOnlineLibrary.Application.Interfaces.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoahOnlineLibrary.Application.Services
{
    internal class Animation : IAnimationService
    {
        public void EnterTheLibrary()
        {
            string[] noah =
            {
                public void EnterTheArk()
        {
            string[] noah =
            {
        @"  ======================================================",
        @"    W E L C O M E    T O   N O A H ' S    L I B R A R Y ",
        @"  ======================================================",
        @"  |____________________________________________________|",
        @"  | __     __   ____   ___ ||  ____    ____     _  __  |",
        @"  ||  |__ |--|_| || |_|   |||_|**|*|__|+|+||___| ||  | |",
        @"  ||==|^^||--| |=||=| |=*=||| |~~|~|  |=|=|| | |~||==| |",
        @"  ||  |##||  | | || | |JRO|||-|  | |==|+|+||-|-|~||__| |",
        @"  ||__|__||__|_|_||_|_|___|||_|__|_|__|_|_||_|_|_||__|_|",
        @"  ||_______________________||__________________________|",
        @"  | _____________________  ||  _______________________ |",
        @"  ||=|=|=|=|=|=|=|=|=|   ,-----.     |_|  ||#||==|  / /|",
        @"  || | | | | | | | |    /,-. ,-.\    |=|  || ||==| / / |",
        @"  ||_|_|_|_|_|_|_|_|   ()> o o <()   __|__||_||__|/_/__|",
        @"  |________________    (.--(_)--.)    _________________|",
        @"  | __   __          ,'/.-'\_/`-.\`.      ___     _____|",
        @"  ||~~|_|..|__|    ,' /    `-'    \ `.  ||~|~|___| | | |",
        @"  ||--|+|^^|==|   /   \           /   \   x|x|+|+|=|=|=|",
        @"  ||__|_|__|__|  /     `.       ,'     \   |_|_|_|_|_|_|",
        @"  |__________   /    /   `-._.-'   \    \   ___________|",
        @"  | _____     ,-`-._/|    -| |o    |\_.-<      ________|",
        @"  ||_____|_  <,--.)  |____-|_|o____|  )_ \  |_|++|_|-|||",
        @"  ||______|           |//  / \  \\|     )/  |~|  | | |||",
        @"  ||______||_|_|      |'   | |   `|         |_|__|_|_|||",
        @"  |____________________________________________________|",
        @"  |__    _  /    ________     ______           /| _ _ _|",
        @"  |\ \  |=|/   //    /| //   /  /  / |        / ||%|%|%|",
        @"  | \/\ |*/  .//____//.//   /__/__/ (_)      /  ||=|=|=|",
        @"__|  \/\|/   /(____|/ //                    /  /||~|~|~|",
        @"  |___\_/   /________//     ~ P A ~        /  / ||_|_|_|",
        @"  |___ /   (|________/    ~ 3  0  2 ~     /  /| |______|",
        @"      /                                  /  / | |",
        @"     /__________________________________/  /  | |",
        @"     \__________________________________\_/"
    };

            Console.CursorVisible = false;

            int startTop = Console.WindowHeight;

            for (int top = startTop; top >= 2; top--)
            {
                Console.Clear();

                Console.ForegroundColor = ConsoleColor.DarkYellow;

                for (int i = 0; i < noah.Length; i++)
                {
                    int currentLine = top + i;

                    if (currentLine >= 0 && currentLine < Console.WindowHeight)
                    {
                        Console.SetCursorPosition(5, currentLine);
                        Console.WriteLine(noah[i]);
                    }
                }

                Console.ResetColor();

                Thread.Sleep(100);
            }

            Thread.Sleep(4000);

            Console.CursorVisible = true;
        }
        //ANIMATIN ASCRII ART YERLESDIR
    };

            Console.CursorVisible = false;

            int startTop = Console.WindowHeight;

            for (int top = startTop; top >= 2; top--)
            {
                Console.Clear();

                Console.ForegroundColor = ConsoleColor.DarkYellow;

                for (int i = 0; i < noah.Length; i++)
                {
                    int currentLine = top + i;

                    if (currentLine >= 0 && currentLine < Console.WindowHeight)
                    {
                        Console.SetCursorPosition(5, currentLine);
                        Console.Write(noah[i]);
                    }
                }

                Console.ResetColor();

                Thread.Sleep(100);
            }

            Thread.Sleep(4000);

            Console.Clear();
            Console.CursorVisible = true;
        }

    }
}
