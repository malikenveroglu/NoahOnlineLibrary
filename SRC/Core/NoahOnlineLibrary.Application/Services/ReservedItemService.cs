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
                    List<ReservedItem> conflictingReservations = reservedItems
                        .Where(r =>
                            r.BookId == selectedBook.Id &&
                            r.Status != Status.Completed &&
                            r.Status != Status.Canceled &&
                            startDate <= r.EndDate &&
                            endDate >= r.StartDate)
                        .OrderBy(r => r.StartDate)
                        .ToList();

                    if (conflictingReservations.Count == 0)
                        break;

                    Console.Clear();

                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Library Error:");
                    Console.WriteLine("This Book Is Already Reserved For Date You Choose For Selected Dates");
                    Console.ResetColor();
                    Console.WriteLine();
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("Current Reservation Dates:");
                    Console.ResetColor();
                    foreach (ReservedItem reservation in conflictingReservations)
                    {
                        Console.Write($"{reservation.StartDate:dd.MM.yyyy} - {reservation.EndDate:dd.MM.yyyy} (");

                        switch (reservation.Status)
                        {
                            case Status.Confirmed:
                                Console.ForegroundColor = ConsoleColor.Yellow;
                                break;

                            case Status.Started:
                                Console.ForegroundColor = ConsoleColor.Green;
                                break;
                        }

                        Console.Write(reservation.Status);
                        Console.ResetColor();

                        Console.WriteLine(")");
                    }

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
            while (true)
            {
                Console.Clear();

                List<ReservedItem> reservedItems = _reservedItemRepository.GetAll();
                List<Book> books = _bookRepository.GetAll();

                if (!reservedItems.Any())
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("Library Error: There Are No Reservations Yet.");
                    Console.ResetColor();

                    Console.WriteLine("\nPress Any Key To Back To Menu...");
                    Console.ReadKey();
                    return;
                }

                while (true)
                {
                    Console.Clear();

                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("========== RESERVATION LIST ==========");
                    Console.ResetColor();

                    Console.WriteLine();
                    Console.WriteLine("Sort Reservations By:");
                    Console.WriteLine();
                    Console.WriteLine("1. Status");
                    Console.WriteLine("-------------------");
                    Console.WriteLine("2. Fin Code");
                    Console.WriteLine("-------------------");
                    Console.WriteLine("3. Start Date");
                    Console.WriteLine("-------------------");
                    Console.WriteLine("4. End Date");
                    Console.WriteLine("-------------------");
                    Console.WriteLine("5. Without Sorting");
                    Console.WriteLine("-------------------");
                    Console.WriteLine();
                    Console.WriteLine("Press 'M' To Back To Menu.");

                    Console.Write("\nSelect Option: ");

                    string input = Console.ReadLine()?.Trim() ?? string.Empty;

                    if (input.ToLower() == "m")
                    {
                        Console.Clear();
                        return;
                    }

                    Console.Clear();
                    switch (input)
                    {
                        case "1":
                            reservedItems = reservedItems
                                .OrderBy(r => r.Status)
                                .ToList();
                            break;

                        case "2":
                            reservedItems = reservedItems
                                .OrderBy(r => r.FinCode)
                                .ToList();
                            break;

                        case "3":
                            reservedItems = reservedItems
                                .OrderBy(r => r.StartDate)
                                .ToList();
                            break;

                        case "4":
                            reservedItems = reservedItems
                                .OrderBy(r => r.EndDate)
                                .ToList();
                            break;

                        case "5":
                            break;

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

                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine(new string('=', 95));
                Console.WriteLine($"{"ID",-5}{"Book",-30}{"FinCode",-12}{"Start",-15}{"End",-15}{"Status",-15}");
                Console.WriteLine(new string('=', 95));
                Console.ResetColor();

                foreach (ReservedItem reservation in reservedItems)
                {
                    Book? book = books.FirstOrDefault(b => b.Id == reservation.BookId);

                    string bookName = book?.Name ?? "Unknown";

                    if (bookName.Length > 28)
                        bookName = bookName.Substring(0, 25) + "...";

                    string startDate = reservation.StartDate.ToString("dd.MM.yyyy");
                    string endDate = reservation.EndDate.ToString("dd.MM.yyyy");

                    Console.Write($"{reservation.Id,-5}");
                    Console.Write($"{bookName,-30}");
                    Console.Write($"{reservation.FinCode,-12}");
                    Console.Write($"{startDate,-15}");
                    Console.Write($"{endDate,-15}");

                    switch (reservation.Status)
                    {
                        case Status.Confirmed:
                            Console.ForegroundColor = ConsoleColor.Yellow;
                            break;

                        case Status.Started:
                            Console.ForegroundColor = ConsoleColor.Green;
                            break;

                        case Status.Completed:
                            Console.ForegroundColor = ConsoleColor.Cyan;
                            break;

                        case Status.Canceled:
                            Console.ForegroundColor = ConsoleColor.Red;
                            break;
                    }

                    Console.WriteLine($"{reservation.Status,-15}");
                    Console.ResetColor();
                    Console.WriteLine("~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~");

                }

                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine(new string('=', 95));
                Console.ResetColor();

                while (true)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("1. Sort Again");
                    Console.WriteLine("2. Back To Menu");
                    Console.ResetColor();

                    Console.Write("\nSelect Option: ");

                    string choice = Console.ReadLine()?.Trim() ?? string.Empty;

                    switch (choice)
                    {
                        case "1":
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
                            Console.Clear();
                            continue;
                    }

                    break;
                }
            }
        }

        public void ChangeReservationStatus()
        {
            while (true)
            {
                Console.Clear();

                List<ReservedItem> reservedItems = _reservedItemRepository.GetAll();
                List<Book> books = _bookRepository.GetAll();

                if (reservedItems.Count == 0)
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("Library Error: There Are No Reservations Yet.");
                    Console.ResetColor();

                    Console.WriteLine("\nPress Any Key To Back To Menu...");
                    Console.ReadKey();
                    return;
                }

                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine(new string('=', 95));
                Console.WriteLine($"{"ID",-5}{"Book",-30}{"FinCode",-12}{"Start",-15}{"End",-15}{"Status",-15}");
                Console.WriteLine(new string('=', 95));
                Console.ResetColor();

                foreach (ReservedItem reservation in reservedItems)
                {
                    Book? book = books.FirstOrDefault(b => b.Id == reservation.BookId);

                    string bookName = book?.Name ?? "Unknown Book";

                    if (bookName.Length > 28)
                        bookName = bookName.Substring(0, 25) + "...";

                    string startDate = reservation.StartDate.ToString("dd.MM.yyyy");
                    string endDate = reservation.EndDate.ToString("dd.MM.yyyy");

                    Console.Write($"{reservation.Id,-5}");
                    Console.Write($"{bookName,-30}");
                    Console.Write($"{reservation.FinCode,-12}");
                    Console.Write($"{startDate,-15}");
                    Console.Write($"{endDate,-15}");

                    switch (reservation.Status)
                    {
                        case Status.Confirmed:
                            Console.ForegroundColor = ConsoleColor.Yellow;
                            break;

                        case Status.Started:
                            Console.ForegroundColor = ConsoleColor.Green;
                            break;

                        case Status.Completed:
                            Console.ForegroundColor = ConsoleColor.Cyan;
                            break;

                        case Status.Canceled:
                            Console.ForegroundColor = ConsoleColor.Red;
                            break;
                    }

                    Console.WriteLine($"{reservation.Status,-15}");
                    Console.ResetColor();
                    Console.WriteLine("~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~");
                }

                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine(new string('=', 95));
                Console.ResetColor();

                ReservedItem? selectedReservation;

                while (true)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.Write("\nEnter Reservation Id\nPress 'M' To Back To Menu: ");
                    Console.ResetColor();

                    string input = Console.ReadLine()?.Trim() ?? string.Empty;

                    if (input.ToLower() == "m")
                    {
                        Console.Clear();
                        return;
                    }

                    if (!int.TryParse(input, out int reservationId))
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("\nLibrary Error: Reservation Id Must Be A Number.");
                        Console.ResetColor();

                        Console.WriteLine("\nPress Any Key To Try Again...");
                        Console.ReadKey();
                        continue;
                    }

                    selectedReservation = reservedItems.FirstOrDefault(r => r.Id == reservationId);

                    if (selectedReservation == null)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("\nLibrary Error: Reservation Not Found.");
                        Console.ResetColor();

                        Console.WriteLine("\nPress Any Key To Try Again...");
                        Console.ReadKey();
                        continue;
                    }

                    break;
                }

                Book? selectedBook = books.FirstOrDefault(b => b.Id == selectedReservation.BookId);

                Console.Clear();

                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine(new string('=', 95));
                Console.WriteLine($"{"ID",-5}{"Book",-30}{"FinCode",-12}{"Start",-15}{"End",-15}{"Status",-15}");
                Console.WriteLine(new string('=', 95));
                Console.ResetColor();

                string selectedBookName = selectedBook?.Name ?? "Unknown Book";

                if (selectedBookName.Length > 28)
                    selectedBookName = selectedBookName.Substring(0, 25) + "...";

                Console.Write($"{selectedReservation.Id,-5}");
                Console.Write($"{selectedBookName,-30}");
                Console.Write($"{selectedReservation.FinCode,-12}");
                string selectedStartDate = selectedReservation.StartDate.ToString("dd.MM.yyyy");
                string selectedEndDate = selectedReservation.EndDate.ToString("dd.MM.yyyy");

                Console.Write($"{selectedStartDate,-15}");
                Console.Write($"{selectedEndDate,-15}");

                switch (selectedReservation.Status)
                {
                    case Status.Confirmed:
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        break;

                    case Status.Started:
                        Console.ForegroundColor = ConsoleColor.Green;
                        break;

                    case Status.Completed:
                        Console.ForegroundColor = ConsoleColor.Cyan;
                        break;

                    case Status.Canceled:
                        Console.ForegroundColor = ConsoleColor.Red;
                        break;
                }

                Console.WriteLine($"{selectedReservation.Status,-15}");

                Console.ResetColor();

                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine(new string('=', 95));
                Console.ResetColor();

                while (true)
                {
                    if (selectedReservation.Status == Status.Completed ||
                        selectedReservation.Status == Status.Canceled)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("\nLibrary Error: This Reservation Can Not Be Changed Anymore.");
                        Console.ResetColor();

                        Console.WriteLine("\nPress Any Key To Back To Menu...");
                        Console.ReadKey();
                        return;
                    }

                    Console.WriteLine();

                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("Select New Status:");
                    Console.ResetColor();

                    if (selectedReservation.Status == Status.Confirmed)
                    {
                        Console.ForegroundColor = ConsoleColor.Blue;
                        Console.WriteLine("1. Started");
                        Console.WriteLine("2. Canceled");
                        Console.ResetColor();
                    }
                    else if (selectedReservation.Status == Status.Started)
                    {
                        Console.ForegroundColor = ConsoleColor.Blue;
                        Console.WriteLine("1. Completed");
                        Console.ResetColor();
                    }

                    Console.Write("\nPress 'M' To Back To Menu: ");

                    string input = Console.ReadLine()?.Trim() ?? string.Empty;

                    if (input.ToLower() == "m")
                    {
                        Console.Clear();
                        return;
                    }

                    Status newStatus;

                    if (selectedReservation.Status == Status.Confirmed)
                    {
                        switch (input)
                        {
                            case "1":
                                newStatus = Status.Started;
                                break;

                            case "2":
                                newStatus = Status.Canceled;
                                break;

                            default:

                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine("\nLibrary Error: Invalid Choice.");
                                Console.ResetColor();

                                Console.WriteLine("\nPress Any Key To Try Again...");
                                Console.ReadKey();
                                Console.Clear();
                                continue;
                        }
                    }
                    else
                    {
                        if (input != "1")
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("\nLibrary Error: Invalid Choice.");
                            Console.ResetColor();

                            Console.WriteLine("\nPress Any Key To Try Again...");
                            Console.ReadKey();
                            Console.Clear();
                            continue;
                        }

                        newStatus = Status.Completed;
                    }

                    selectedReservation.Status = newStatus;

                    _reservedItemRepository.Update(selectedReservation);
                    _reservedItemRepository.SaveChanges();

                    break;
                }

                while (true)
                {
                    Console.Clear();

                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("Congratulations: Reservation Status Successfully Changed.");
                    Console.ResetColor();
                    Console.WriteLine();

                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("=========================================");
                    Console.WriteLine($"{"ID",-6}{"Book Name",-15}{"Reservation Status"}");
                    Console.WriteLine("=========================================");
                    Console.ResetColor();

                    Console.WriteLine($"{selectedReservation.Id,-6}{selectedBook?.Name,-15}{selectedReservation.Status}");
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("=========================================");
                    Console.ResetColor();
                    Console.WriteLine();

                    Console.WriteLine("1. Change Another Reservation");
                    Console.WriteLine("2. Back To Menu");

                    Console.Write("\nSelect Option: ");

                    string choice = Console.ReadLine()?.Trim() ?? string.Empty;

                    switch (choice)
                    {
                        case "1":
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
            }
        }

        public void UsersReservationList()
        {
            while (true)
            {
                Console.Clear();

                List<ReservedItem> reservedItems = _reservedItemRepository.GetAll();
                List<Book> books = _bookRepository.GetAll();

                if (reservedItems.Count == 0)
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("Library Error: There Are No Reservations Yet.");
                    Console.ResetColor();

                    Console.WriteLine("\nPress Any Key To Back To Menu...");
                    Console.ReadKey();
                    return;
                }   

                string finCode;

                while (true)
                {
                    Console.Clear();

                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("========== USER'S RESERVATIONS ==========");
                    Console.ResetColor();

                    List<string> finCodes = reservedItems
                        .Select(r => r.FinCode)
                        .Distinct()
                        .OrderBy(f => f)
                        .ToList();

                    Console.WriteLine();

                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine(new string('=', 25));
                    Console.WriteLine($"{"No",-5}{"FinCode",-15}");
                    Console.WriteLine(new string('=', 25));
                    Console.ResetColor();

                    int index = 1;

                    foreach (string code in finCodes)
                    {
                        Console.WriteLine($"{index++,-5}{code,-15}");
                    }

                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine(new string('=', 25));
                    Console.ResetColor();

                    Console.WriteLine();
                    Console.WriteLine("Enter FinCode");
                    Console.WriteLine("Press 'M' To Back To Menu:");
                    Console.WriteLine();

                    string input = Console.ReadLine()?.Trim().ToUpper() ?? string.Empty;

                    if (input.ToLower() == "m")
                    {
                        Console.Clear();
                        return;
                    }

                    if (string.IsNullOrWhiteSpace(input))
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("\nLibrary Error: FinCode Can Not Be Empty.");
                        Console.ResetColor();

                        Console.WriteLine("\nPress Any Key To Try Again...");
                        Console.ReadKey();
                        continue;
                    }

                    if (input.Length != 7)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("\nLibrary Error: FinCode Must Be 7 Characters.");
                        Console.ResetColor();

                        Console.WriteLine("\nPress Any Key To Try Again...");
                        Console.ReadKey();
                        continue;
                    }

                    if (!input.All(char.IsLetterOrDigit))
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("\nLibrary Error: FinCode Can Contain Only Letters And Numbers.");
                        Console.ResetColor();

                        Console.WriteLine("\nPress Any Key To Try Again...");
                        Console.ReadKey();
                        continue;
                    }

                    finCode = input;
                    break;
                }

                List<ReservedItem> userReservations = reservedItems
                    .Where(r => r.FinCode == finCode)
                    .ToList();

                if (userReservations.Count == 0)
                {
                    while (true)
                    {
                        Console.Clear();

                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Library Error:");
                        Console.WriteLine("No Reservations Found For This FinCode.");
                        Console.ResetColor();

                        Console.WriteLine();
                        Console.WriteLine("1. Try Again");
                        Console.WriteLine("2. Back To Menu");

                        Console.Write("\nSelect Option: ");

                        string choice = Console.ReadLine()?.Trim() ?? string.Empty;

                        switch (choice)
                        {
                            case "1":
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

                    continue;
                }

                Console.Clear();

                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("========== USER'S RESERVATIONS ==========");
                Console.ResetColor();

                Console.WriteLine();
                Console.WriteLine($"FinCode             : {finCode}");
                Console.WriteLine($"Total Reservations  : {userReservations.Count}");
                Console.WriteLine();

                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine(new string('=', 95));
                Console.WriteLine($"{"ID",-5}{"Book",-30}{"FinCode",-12}{"Start",-15}{"End",-15}{"Status",-15}");
                Console.WriteLine(new string('=', 95));
                Console.ResetColor();

                foreach (ReservedItem reservation in userReservations)
                {
                    Book? book = books.FirstOrDefault(b => b.Id == reservation.BookId);

                    string bookName = book?.Name ?? "Unknown Book";

                    if (bookName.Length > 28)
                        bookName = bookName.Substring(0, 25) + "...";

                    string startDate = reservation.StartDate.ToString("dd.MM.yyyy");
                    string endDate = reservation.EndDate.ToString("dd.MM.yyyy");

                    Console.Write($"{reservation.Id,-5}");
                    Console.Write($"{bookName,-30}");
                    Console.Write($"{reservation.FinCode,-12}");
                    Console.Write($"{startDate,-15}");
                    Console.Write($"{endDate,-15}");

                    switch (reservation.Status)
                    {
                        case Status.Confirmed:
                            Console.ForegroundColor = ConsoleColor.Yellow;
                            break;

                        case Status.Started:
                            Console.ForegroundColor = ConsoleColor.Green;
                            break;

                        case Status.Completed:
                            Console.ForegroundColor = ConsoleColor.Cyan;
                            break;

                        case Status.Canceled:
                            Console.ForegroundColor = ConsoleColor.Red;
                            break;
                    }

                    Console.WriteLine($"{reservation.Status,-15}");
                    Console.ResetColor();
                }

                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine(new string('=', 95));
                Console.ResetColor();

                while (true)
                {
                    Console.WriteLine();
                    Console.WriteLine("1. Search Another FinCode");
                    Console.WriteLine("2. Back To Menu");

                    Console.Write("\nSelect Option: ");

                    string choice = Console.ReadLine()?.Trim() ?? string.Empty;

                    switch (choice)
                    {
                        case "1":
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
                            Console.Clear();
                            continue;
                    }

                    break;
                }
            }
        }
    }
}
