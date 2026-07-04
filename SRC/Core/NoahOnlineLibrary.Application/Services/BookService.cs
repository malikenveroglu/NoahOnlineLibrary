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
    public class BookService: IBookService
    {
        private readonly IBookRepository _bookRepository;
        private readonly IAuthorRepository _authorRepository;


        public BookService(IBookRepository bookRepository, IAuthorRepository authorRepository)
        {
          _bookRepository = bookRepository;  
          _authorRepository = authorRepository;
        }

        public void CreateBook()
        {
            while (true)
            {
                Console.Clear();

                var authors = _authorRepository.GetAll();

                string name;

                while (true)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("Press Enter To Type Book Name / Press 'M' To Back To Menu:");
                    Console.ResetColor();

                    string bookName = Console.ReadLine()?.Trim() ?? string.Empty;

                    if (bookName.ToLower() == "m")
                    {
                        Console.Clear();
                        return;
                    }

                    if (string.IsNullOrWhiteSpace(bookName))
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Library Error: Book Name Can Not Be Empty.");
                        Console.ResetColor();

                        Console.WriteLine("Press Any Key To Try Again...");
                        Console.ReadKey();
                        Console.Clear();
                        continue;
                    }

                    name = char.ToUpper(bookName[0]) + bookName.Substring(1).ToLower();
                    break;
                }

                int pageCount;

                while (true)
                {
                    Console.Clear();

                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("Enter Page Count / Press 'M' To Back To Menu:");
                    Console.ResetColor();

                    string pageInput = Console.ReadLine()?.Trim() ?? string.Empty;

                    if (pageInput.ToLower() == "m")
                    {
                        Console.Clear();
                        return;
                    }

                    if (!int.TryParse(pageInput, out int pages) || pages <= 0)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Library Error: Page Count Must Be A Positive Or A Number.");
                        Console.ResetColor();

                        Console.WriteLine("Press Any Key To Try Again...");
                        Console.ReadKey();
                        continue;
                    }

                    pageCount = pages;
                    break;
                }

                int authorId;
                Author? author;

                while (true)
                {
                    Console.Clear();

                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("Select Author For This Book:");
                    Console.ResetColor();

                    foreach (var a in authors)
                    {
                        Console.WriteLine($"{a.Id}. {a.Name} {a.Surname}");
                    }

                    Console.WriteLine();
                    Console.Write("Enter Author Id / Press 'M' To Back To Menu: ");

                    string authorInput = Console.ReadLine()?.Trim() ?? string.Empty;

                    if (authorInput.ToLower() == "m")
                    {
                        Console.Clear();
                        return;
                    }

                    if (!int.TryParse(authorInput, out int id))
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Library Error: Author Id Must Be A Number.");
                        Console.ResetColor();

                        Console.WriteLine("Press Any Key To Try Again...");
                        Console.ReadKey();
                        continue;
                    }

                    author = authors.FirstOrDefault(a => a.Id == id);

                    if (author == null)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Library Error: Author Not Found.");
                        Console.ResetColor();

                        Console.WriteLine("Press Any Key To Try Again...");
                        Console.ReadKey();
                        continue;
                    }

                    authorId = id;
                    break;
                }

                Book book = new Book
                {
                    Name = name,
                    PageCount = pageCount,
                    AuthorId = authorId
                };

                _bookRepository.Add(book);
                _bookRepository.SaveChanges();

                bool createAgain = false;

                while (true)
                {
                    Console.Clear();

                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("Congratulations: The Book Was Successfully Created:");
                    Console.ResetColor();

                    Console.WriteLine(" ");
                    Console.WriteLine($"ID: {book.Id} \nName: {book.Name} \nPage Count: {book.PageCount} \nAuthorId: {book.AuthorId}");
                    Console.WriteLine(" ");

                    Console.WriteLine("1. Create One More Book");
                    Console.WriteLine("2. Back To Menu");

                    string choice = Console.ReadLine()?.Trim() ?? string.Empty;

                    switch (choice)
                    {
                        case "1":
                            createAgain = true;
                            break;

                        case "2":
                            Console.Clear();
                            return;

                        default:
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("Library Error: Invalid Choice.");
                            Console.ResetColor();

                            Console.WriteLine("Press Any Key To Try Again...");
                            Console.ReadKey();
                            continue;
                    }

                    break;
                }

                if (createAgain)
                    continue;
            }
        }

        public void DeleteBook()
        {
            while (true)
            {
                Console.Clear();

                var books = _bookRepository.GetAll();

                if (books.Count == 0)
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("No books found in library.");
                    Console.ResetColor();

                    Console.WriteLine("\nPress any key to return to menu...");
                    Console.ReadKey();
                    return;
                }

                int bookId;

                while (true)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("=== DELETE BOOK ===");
                    Console.WriteLine("Select Book To Delete / Press 'M' To Back To Menu:");
                    Console.ResetColor();
                    Console.WriteLine(" ");

                    foreach (var b in books)
                    {
                        Console.WriteLine($"{b.Id}. {b.Name}");
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
                        Console.Clear();
                        continue;
                    }

                    var book = books.FirstOrDefault(b => b.Id == id);

                    if (book == null)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("\nLibrary Error: Book Not Found.");
                        Console.ResetColor();

                        Console.WriteLine("\nPress Any Key To Try Again...");
                        Console.ReadKey();
                        Console.Clear();
                        continue;
                    }

                    bookId = id;
                    break;
                }

                var selectedBook = books.First(b => b.Id == bookId);

                while (true)
                {
                    Console.Clear();

                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("\nYou are about to delete this book:");
                    Console.ResetColor();
                    Console.WriteLine(" ");
                    Console.WriteLine($"ID: {selectedBook.Id}");
                    Console.WriteLine($"Name: {selectedBook.Name}");
                    Console.WriteLine($"Page Count: {selectedBook.PageCount}");

                    Console.WriteLine("\n1. Confirm Delete");
                    Console.WriteLine("2. Back To Menu");

                    Console.Write("\nSelect option: ");

                    string choice = Console.ReadLine()?.Trim() ?? string.Empty;

                    switch (choice)
                    {
                        case "1":
                            _bookRepository.Delete(selectedBook);
                            _bookRepository.SaveChanges();

                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.WriteLine("\nBook successfully deleted!");
                            Console.ResetColor();

                            Console.WriteLine("\nPress any key to continue...");
                            Console.ReadKey();

                            return;

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
            }
        }

        public void GetBookById()
        {
            while (true)
            {
                Console.Clear();

                List<Book> books = _bookRepository.GetAll();
                List<Author> authors = _authorRepository.GetAll();

                if (books.Count == 0)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("\nLibrary Error: There Are No Books In The Library.");
                    Console.ResetColor();

                    Console.WriteLine();
                    Console.WriteLine("Press Any Key To Return To Menu...");
                    Console.ReadKey();
                    Console.Clear();
                    return;
                }

                Book? selectedBook = null;

                while (true)
                {
                    Console.Clear();

                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("AVAILABLE BOOKS");
                    Console.ResetColor();
                    Console.WriteLine();

                    foreach (Book book in books)
                    {
                        Console.WriteLine($"{book.Id}. {book.Name}");
                    }

                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("\nType Book Id / Press 'M' To Back To Menu:");
                    Console.ResetColor();

                    string input = Console.ReadLine()?.Trim() ?? string.Empty;

                    if (input.ToLower() == "m")
                    {
                        Console.Clear();
                        return;
                    }

                    if (!int.TryParse(input, out int bookId))
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("\nLibrary Error: Book Id Must Be A Number.");
                        Console.ResetColor();

                        Console.WriteLine("\nPress Any Key To Try Again...");
                        Console.ReadKey();
                        continue;
                    }

                    selectedBook = books.FirstOrDefault(b => b.Id == bookId);

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

                Author? author = authors.FirstOrDefault(a => a.Id == selectedBook.AuthorId);

                bool searchAgain = false;

                while (true)
                {
                    Console.Clear();

                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("BOOK INFORMATION");
                    Console.ResetColor();

                    Console.WriteLine();

                    Console.WriteLine($"ID: {selectedBook.Id}");
                    Console.WriteLine($"Name: {selectedBook.Name}");
                    Console.WriteLine($"Page Count: {selectedBook.PageCount}");
                    Console.WriteLine($"Author: {author?.Name} {author?.Surname}");

                    Console.WriteLine();
                    Console.WriteLine("1. Search Another Book");
                    Console.WriteLine("2. Back To Menu");

                    string choice = Console.ReadLine()?.Trim() ?? string.Empty;

                    switch (choice)
                    {
                        case "1":
                            searchAgain = true;
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

                if (searchAgain)
                {
                    continue;
                }
            }
        }
    }
}
