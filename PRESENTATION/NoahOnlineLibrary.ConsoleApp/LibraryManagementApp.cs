using NoahOnlineLibrary.Application.Interfaces.IRepository;
using NoahOnlineLibrary.Application.Interfaces.IServices;
using NoahOnlineLibrary.Application.Services;
using NoahOnlineLibrary.ConsoleApp.UI;
using NoahOnlineLibrary.Domain.Entities;
using NoahOnlineLibrary.Persistence.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoahOnlineLibrary.ConsoleApp
{
    internal class LibraryManagementApp
    {
        private readonly IBookService _bookService;
        private readonly IAuthorService _authorService;
        private readonly IReservedItemService _reservedItemService;
        private AnimationService _animationService = new();

        public LibraryManagementApp(IBookService bookService,IAuthorService authorService,IReservedItemService reservedItemService)
        {
            _bookService = bookService;
            _authorService = authorService;
            _reservedItemService = reservedItemService;
        }

        public void Run()
        {
            _animationService.EnterTheLibrary();
            ConsoleTheme.Apply();

            while (true)
            {
                Console.Clear();

                MainMenuUI.Draw();

                Console.SetCursorPosition(84, 19);

                string choice = Console.ReadLine()?.Trim() ?? "";

                switch (choice)
                {
                    case "1":
                        BookManagementMenu();
                        break;

                    case "2":
                        AuthorManagementMenu();
                        break;

                    case "3":
                        ReservationManagementMenu();
                        break;

                    case "0":
                        Console.Clear();
                        _animationService.LeaveTheLibrary();
                        return;

                    default:
                        Console.ForegroundColor = ConsoleColor.Red;

                        Console.SetCursorPosition(68, 22);
                        Console.Write("Library Error: Invalid Choice.");

                        Console.SetCursorPosition(68, 24);
                        Console.Write("Press Any Key...");

                        Console.ResetColor();

                        Console.ReadKey();
                        break;
                }
            }
        }

        private void BookManagementMenu()
        {
            while (true)
            {
                Console.Clear();

                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("=========================================");
                Console.WriteLine("         BOOK MANAGEMENT");
                Console.WriteLine("=========================================");
                Console.ResetColor();

                Console.WriteLine();
                Console.WriteLine("1. Create Book");
                Console.WriteLine();
                Console.WriteLine("2. Delete Book");
                Console.WriteLine();
                Console.WriteLine("3. Get Book By Id");
                Console.WriteLine();
                Console.WriteLine("0. Back To Main Menu");

                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine();
                Console.WriteLine("=========================================");
                Console.ResetColor();

                Console.Write("\nSelect Option: ");

                string choice = Console.ReadLine()?.Trim() ?? string.Empty;

                switch (choice)
                {
                    case "1":
                        _bookService.CreateBook();
                        break;

                    case "2":
                        _bookService.DeleteBook();
                        break;

                    case "3":
                        _bookService.GetBookById();
                        break;

                    case "0":
                        return;

                    default:
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("\nLibrary Error: Invalid Choice.");
                        Console.ResetColor();

                        Console.WriteLine("\nPress Any Key To Try Again...");
                        Console.ReadKey();
                        break;
                }
            }
        }

        private void AuthorManagementMenu()
        {
            while (true)
            {
                Console.Clear();

                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("=========================================");
                Console.WriteLine("        AUTHOR MANAGEMENT");
                Console.WriteLine("=========================================");
                Console.ResetColor();

                Console.WriteLine();
                Console.WriteLine("1. Create Author");
                Console.WriteLine();
                Console.WriteLine("2. Delete Author");
                Console.WriteLine();
                Console.WriteLine("3. Show All Authors");
                Console.WriteLine();
                Console.WriteLine("0. Back To Main Menu");

                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine();
                Console.WriteLine("=========================================");
                Console.ResetColor();

                Console.Write("\nSelect Option: ");

                string choice = Console.ReadLine()?.Trim() ?? string.Empty;

                switch (choice)
                {
                    case "1":
                        _authorService.CreateAuthor();
                        break;

                    case "2":
                        _authorService.DeleteAuthor();
                        break;

                    case "3":
                        _authorService.ShowAllAuthors();
                        break;

                    case "0":
                        return;

                    default:
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("\nLibrary Error: Invalid Choice.");
                        Console.ResetColor();

                        Console.WriteLine("\nPress Any Key To Try Again...");
                        Console.ReadKey();
                        break;
                }
            }
        }

        private void ReservationManagementMenu()
        {
            while (true)
            {
                Console.Clear();

                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("=========================================");
                Console.WriteLine("     RESERVATION MANAGEMENT");
                Console.WriteLine("=========================================");
                Console.ResetColor();

                Console.WriteLine();
                Console.WriteLine("1. Reserve Book");
                Console.WriteLine();
                Console.WriteLine("2. Reservation List");
                Console.WriteLine();
                Console.WriteLine("3. Change Reservation Status");
                Console.WriteLine();
                Console.WriteLine("4. User Reservation List");
                Console.WriteLine();
                Console.WriteLine("0. Back To Main Menu");

                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine();
                Console.WriteLine("=========================================");
                Console.ResetColor();

                Console.Write("\nSelect Option: ");

                string choice = Console.ReadLine()?.Trim() ?? string.Empty;

                switch (choice)
                {
                    case "1":
                        _reservedItemService.ReserveBook();
                        break;

                    case "2":
                        _reservedItemService.ReservationList();
                        break;

                    case "3":
                        _reservedItemService.ChangeReservationStatus();
                        break;

                    case "4":
                        _reservedItemService.UsersReservationList();
                        break;

                    case "0":
                        return;

                    default:
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("\nLibrary Error: Invalid Choice.");
                        Console.ResetColor();

                        Console.WriteLine("\nPress Any Key To Try Again...");
                        Console.ReadKey();
                        break;
                }
            }
        }
    }
}
