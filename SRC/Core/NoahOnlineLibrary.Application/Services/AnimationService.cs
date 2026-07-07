using NoahOnlineLibrary.Application.Interfaces.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoahOnlineLibrary.Application.Services
{
    public class AnimationService 
    {
        public void EnterTheLibrary()
        {
            string[] noah =
            {
                @"  ======================================================",
                @"    W E L C O M E   T O    N O A H ' S    L I B R A R Y ",
                @"  ======================================================",
                @"  |____________________________________________________|",
                @"  | __     __   ____   ___ ||  ____    ____     _  __  |",
                @"  ||  |__ |--|_| || |_|   |||_|**|*|__|+|+||___| ||  | |",
                @"  ||==|^^||--| |=||=| |=*=||| |~~|~|  |=|=|| | |~||==| |",
                @"  ||  |##||  | | || | |MLK|||-|  | |==|+|+||-|-|~||__| |",
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
                @"     \__________________________________\ /"
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

            Console.Clear();
            Console.CursorVisible = true;
        }

        public void LeaveTheLibrary()
        {
            for (int i = 0; i < 70; i++)
            {
                Console.Clear();


                Console.WriteLine("K I T A B I N     B A Ğ L A N I B");
                Console.WriteLine(" ");
                Console.WriteLine(" ");
                Console.WriteLine(" ");
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(new string(' ', i) + @"       ,--------,            ,------,              ");
                Console.WriteLine(new string(' ', i) + @"       )_________)           )_______)              ");
                Console.WriteLine(new string(' ', i) + @"     _______________         ___________         ");
                Console.WriteLine(new string(' ', i) + @"    )                )      )           )        _____");
                Console.WriteLine(new string(' ', i) + @"    )                 )     )            )      )      ) ");
                Console.WriteLine(new string(' ', i) + @"    )                  )    )             )     )       ) ");
                Console.WriteLine(new string(' ', i) + @"    )___________________)   )______________)    )________)  ");
                Console.WriteLine(new string(' ', i) + @"            |   |                |   |             |  |");
                Console.WriteLine(new string(' ', i) + @"            |   | .--.           |   |   .--- ");
                Console.WriteLine(new string(' ', i) + @"  ___         .---|__|           .-.     |~~~|--|       ___");
                Console.WriteLine(new string(' ', i) + @"  \ `\     |--|===|--|_          |_|     |~~~|--|      / '/");
                Console.WriteLine(new string(' ', i) + @"   \.`\    |  |===|  |'\     .---!~|  .--|   |--|     /.'/");
                Console.WriteLine(new string(' ', i) + @"    \ `\   |%%|   |  |.'\    |===| |--|%%|   |  |    / '/");
                Console.WriteLine(new string(' ', i) + @"     \.`\  |%%|   |  |\.'\   |   | |__|  |   |  |   /. / ");
                Console.WriteLine(new string(' ', i) + @"      \ `\ |  |   |  | \  \  |===| |==|  |   |  |  /  /");
                Console.WriteLine(new string(' ', i) + @"       \.`\|  |   |__|  \.'\ |   |_|__|  |~~~|__| /.'/");
                Console.WriteLine(new string(' ', i) + @"        \  |  |===|--|   \.'\|===|~|--|%%|~~~|--|/  /");
                Console.WriteLine(new string(' ', i) + @"         --^--^---'--^    `-'`---^-^--^--^---'--'--  ");
                Console.ResetColor();
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine(new string(' ', i) + @"   ^^^^`   ^^^^^    ^^^^    ^^^^^     ^^^^      ^^^^   ^^^");
                Console.WriteLine(new string(' ', i) + @"      ^^^^      ^^^^     ^^^^     ^^^       ^^");
                Console.WriteLine(new string(' ', i) + @"   ^^^^    ^^^^      ^^^      ^^^^     ^^^^      ^^^^  ");

                Console.ResetColor();

                Thread.Sleep(80);
            }

            Console.Clear();

            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("The Ark Disappeared In The Ocean...");
            Console.ResetColor();

            Thread.Sleep(3000);
        }
    }
}