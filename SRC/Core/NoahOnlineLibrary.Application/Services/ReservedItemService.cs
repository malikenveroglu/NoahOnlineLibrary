using NoahOnlineLibrary.Application.Interfaces.IRepository;
using NoahOnlineLibrary.Application.Interfaces.IServices;
using NoahOnlineLibrary.Domain.Entities;
using NoahOnlineLibrary.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoahOnlineLibrary.Application.Services
{
    public class ReservedItemService: IReservedItemService
    {
        private readonly IReservedItemRepository _reservedItemRepository;
        private readonly IBookRepository _bookRepository;
        private readonly IAuthorRepository _authorRepository;


        public ReservedItemService(IReservedItemRepository reservedItemRepository, IBookRepository bookRepository, IAuthorRepository authorRepository)
        {
            _reservedItemRepository = reservedItemRepository;
            _bookRepository = bookRepository;
            _authorRepository = authorRepository;
        }

        public void ReserveBook()
        {
            while (true)
            {
                Console.Clear();

                List<Book> books = _bookRepository.GetAll();

                if (books.Count == 0)
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("Library Error: There Are No Books In Library.");
                    Console.ResetColor();

                    Console.WriteLine("\nPress Any Key To Back To Menu...");
                    Console.ReadKey();
                    return;
                }

                Book? selectedBook;

                while (true)
                {
                    Console.Clear();

                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("=== RESERVE BOOK ===");
                    Console.WriteLine("Select Book To Reserve / Press 'M' To Back To Menu:");
                    Console.ResetColor();
                    Console.WriteLine();

                    foreach (Book book in books)
                    {
                        Console.WriteLine($"{book.Id}. {book.Name}");
                    }

                    Console.Write("\nEnter Book Id: ");

                    string input = Console.ReadLine()?.Trim() ?? string.Empty;

                    if (input.ToLower() == "m")
                    {
                        Console.Clear();
                        return;
                    }

                    if (!int.TryParse(input, out int id))
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("\nLibrary Error: Book Id Must Be A Number.");
                        Console.ResetColor();

                        Console.WriteLine("\nPress Any Key To Try Again...");
                        Console.ReadKey();
                        continue;
                    }

                    selectedBook = books.FirstOrDefault(b => b.Id == id);

                    if (selectedBook == null)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("\nLibrary Error: Book Not Found.");
                        Console.ResetColor();

                        Console.WriteLine("\nPress Any Key To Try Again...");
                        Console.ReadKey();
                        continue;
                    }

                    break;
                }

                string finCode;

                while (true)
                {
                    Console.Clear();

                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("Enter Fin Code (7 Characters)");
                    Console.WriteLine("Press 'M' To Back To Menu:");
                    Console.ResetColor();

                    string input = Console.ReadLine()?.Trim().ToUpper() ?? string.Empty;

                    if (input.ToLower() == "m")
                    {
                        Console.Clear();
                        return;
                    }

                    if (input.Length != 7 || !input.All(char.IsLetterOrDigit))
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("\nLibrary Error: Fin Code Must Contain Exactly 7 Letters And/Or Digits.");
                        Console.ResetColor();

                        Console.WriteLine("\nPress Any Key To Try Again...");
                        Console.ReadKey();
                        continue;
                    }

                    List<ReservedItem> _reservedItems = _reservedItemRepository.GetAll();
                    int activeReservationCount = _reservedItems.Count(r => 
                        r.FinCode == input &&
                        (r.Status == Status.Confirmed || 
                        r.Status == Status.Started));

                    if (activeReservationCount >= 3)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("\nLibrary Error: A User Can Reserve Maximum 3 Books At The Same Time.");
                        Console.ResetColor();

                        Console.WriteLine("\nPress Any Key To Try Again...");
                        Console.ReadKey();
                        continue;
                    }

                    finCode = input;
                    break;
                }

                DateTime startDate;

                while (true)
                {
                    Console.Clear();

                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("Enter Start Date (dd.MM.yyyy)");
                    Console.WriteLine("Press 'M' To Back To Menu:");
                    Console.ResetColor();

                    string input = Console.ReadLine()?.Trim() ?? string.Empty;

                    if (input.ToLower() == "m")
                    {
                        Console.Clear();
                        return;
                    }

                    if (!DateTime.TryParseExact(
                            input,
                            "dd.MM.yyyy",
                            null,
                            System.Globalization.DateTimeStyles.None,
                            out DateTime date))
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("\nLibrary Error: Invalid Date Format.");
                        Console.ResetColor();

                        Console.WriteLine("\nPlease Use: dd.MM.yyyy");

                        Console.WriteLine("\nPress Any Key To Try Again...");
                        Console.ReadKey();
                        continue;
                    }

                    if (date.Date < DateTime.Today)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("\nLibrary Error: Start Date Can Not Be Earlier Than Today.");
                        Console.ResetColor();

                        Console.WriteLine("\nPress Any Key To Try Again...");
                        Console.ReadKey();
                        continue;
                    }

                    startDate = date.Date;
                    break;
                }

                DateTime endDate;

                while (true)
                {
                    Console.Clear();

                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("Enter End Date (dd.MM.yyyy)");
                    Console.WriteLine("Press 'M' To Back To Menu:");
                    Console.ResetColor();

                    string input = Console.ReadLine()?.Trim() ?? string.Empty;

                    if (input.ToLower() == "m")
                    {
                        Console.Clear();
                        return;
                    }

                    if (!DateTime.TryParseExact(
                            input,
                            "dd.MM.yyyy",
                            null,
                            System.Globalization.DateTimeStyles.None,
                            out DateTime date))
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("\nLibrary Error: Invalid Date Format.");
                        Console.ResetColor();

                        Console.WriteLine("\nPlease Use: dd.MM.yyyy");

                        Console.WriteLine("\nPress Any Key To Try Again...");
                        Console.ReadKey();
                        continue;
                    }

                    if (date.Date < startDate)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("\nLibrary Error: End Date Can Not Be Earlier Than Start Date.");
                        Console.ResetColor();

                        Console.WriteLine("\nPress Any Key To Try Again...");
                        Console.ReadKey();
                        continue;
                    }

                    endDate = date.Date;
                    break;
                }

                List<ReservedItem> reservedItems = _reservedItemRepository.GetAll();

                while (true)
                {
                    bool hasConflict = reservedItems.Any(r =>
                        r.BookId == selectedBook.Id &&
                        r.Status != Status.Completed &&
                        r.Status != Status.Canceled &&
                        startDate <= r.EndDate &&
                        endDate >= r.StartDate);

                    if (!hasConflict)
                        break;

                    Console.Clear();

                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Library Error:");
                    Console.WriteLine("This Book Is Already Reserved For Date You Choose");
                    Console.WriteLine("For Selected Dates");
                    Console.ResetColor();

                    Console.WriteLine();
                    Console.WriteLine("1. Add Date Again");
                    Console.WriteLine("2. Back To Menu");

                    Console.Write("\nSelect Option: ");

                    string choice = Console.ReadLine()?.Trim() ?? string.Empty;

                    switch (choice)
                    {
                        case "1":

                            while (true)
                            {
                                Console.Clear();

                                Console.ForegroundColor = ConsoleColor.Green;
                                Console.WriteLine("Enter Start Date (dd.MM.yyyy)");
                                Console.WriteLine("Press 'M' To Back To Menu:");
                                Console.ResetColor();

                                string input = Console.ReadLine()?.Trim() ?? string.Empty;

                                if (input.ToLower() == "m")
                                {
                                    Console.Clear();
                                    return;
                                }

                                if (!DateTime.TryParseExact(
                                        input,
                                        "dd.MM.yyyy",
                                        null,
                                        System.Globalization.DateTimeStyles.None,
                                        out DateTime date))
                                {
                                    Console.ForegroundColor = ConsoleColor.Red;
                                    Console.WriteLine("\nLibrary Error: Invalid Date Format.");
                                    Console.ResetColor();

                                    Console.WriteLine("\nPress Any Key To Try Again...");
                                    Console.ReadKey();
                                    continue;
                                }

                                if (date.Date < DateTime.Today)
                                {
                                    Console.ForegroundColor = ConsoleColor.Red;
                                    Console.WriteLine("\nLibrary Error: Start Date Can Not Be Earlier Than Today.");
                                    Console.ResetColor();

                                    Console.WriteLine("\nPress Any Key To Try Again...");
                                    Console.ReadKey();
                                    continue;
                                }

                                startDate = date.Date;
                                break;
                            }

                            while (true)
                            {
                                Console.Clear();

                                Console.ForegroundColor = ConsoleColor.Green;
                                Console.WriteLine("Enter End Date (dd.MM.yyyy)");
                                Console.WriteLine("Press 'M' To Back To Menu:");
                                Console.ResetColor();

                                string input = Console.ReadLine()?.Trim() ?? string.Empty;

                                if (input.ToLower() == "m")
                                {
                                    Console.Clear();
                                    return;
                                }

                                if (!DateTime.TryParseExact(
                                        input,
                                        "dd.MM.yyyy",
                                        null,
                                        System.Globalization.DateTimeStyles.None,
                                        out DateTime date))
                                {
                                    Console.ForegroundColor = ConsoleColor.Red;
                                    Console.WriteLine("\nLibrary Error: Invalid Date Format.");
                                    Console.ResetColor();

                                    Console.WriteLine("\nPress Any Key To Try Again...");
                                    Console.ReadKey();
                                    continue;
                                }

                                if (date.Date < startDate)
                                {
                                    Console.ForegroundColor = ConsoleColor.Red;
                                    Console.WriteLine("\nLibrary Error: End Date Can Not Be Earlier Than Start Date.");
                                    Console.ResetColor();

                                    Console.WriteLine("\nPress Any Key To Try Again...");
                                    Console.ReadKey();
                                    continue;
                                }

                                endDate = date.Date;
                                break;
                            }

                            continue;

                        case "2":
                            Console.Clear();
                            return;

                        default:

                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("\nLibrary Error: Invalid Choice.");
                            Console.ResetColor();

                            Console.WriteLine("\nPress Any Key To Try Again...");
                            Console.ReadKey();
                            continue;
                    }
                }

                ReservedItem reservedItem = new ReservedItem
                {
                    FinCode = finCode,
                    StartDate = startDate,
                    EndDate = endDate,
                    BookId = selectedBook.Id,
                    Status = Status.Confirmed
                };

                _reservedItemRepository.Add(reservedItem);
                _reservedItemRepository.SaveChanges();

                bool reserveAgain = false;

                while (true)
                {
                    Console.Clear();

                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("Congratulations: The Book Was Successfully Reserved.");
                    Console.ResetColor();

                    Console.WriteLine();

                    Console.WriteLine($"Book: {selectedBook.Name}");
                    Console.WriteLine($"Fin Code: {reservedItem.FinCode}");
                    Console.WriteLine($"Start Date: {reservedItem.StartDate:dd.MM.yyyy}");
                    Console.WriteLine($"End Date: {reservedItem.EndDate:dd.MM.yyyy}");
                    Console.WriteLine($"Status: {reservedItem.Status}");

                    Console.WriteLine();
                    Console.WriteLine("1. Reserve One More Book");
                    Console.WriteLine("2. Back To Menu");

                    string choice = Console.ReadLine()?.Trim() ?? string.Empty;

                    switch (choice)
                    {
                        case "1":
                            reserveAgain = true;
                            break;

                        case "2":
                            Console.Clear();
                            return;

                        default:

                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("\nLibrary Error: Invalid Choice.");
                            Console.ResetColor();

                            Console.WriteLine("\nPress Any Key To Try Again...");
                            Console.ReadKey();
                            continue;
                    }
                            break;
                }

                    if (reserveAgain)
                    continue;
            }
        
        }
        

        public void ReservationList()
        {

        }

        public void ChangeReservationStatus()
        {
            
        }

        public void UsersReservationList()
        {
            
        }
    }
}
