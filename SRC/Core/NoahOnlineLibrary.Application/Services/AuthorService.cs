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
    public class AuthorService: IAuthorService
    {
        private readonly IAuthorRepository _authorRepository;
        private readonly IBookRepository _bookRepository;


        public AuthorService(IAuthorRepository authorRepository, IBookRepository bookRepository)
        {
           _authorRepository = authorRepository; 
           _bookRepository = bookRepository;
        }

        public void CreateAuthor()
        {
            while (true)
            {
                Console.Clear();
                string name;
                while (true)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("Press Enter To Type Author Name / Press 'M' To Back To Menu:");
                    Console.ResetColor();

                    string authorName = Console.ReadLine()?.Trim() ?? string.Empty;

                    if (authorName.ToLower() == "m")
                    {
                        Console.Clear();
                        return;
                    }

                    if (string.IsNullOrWhiteSpace(authorName))
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Library Error: Author Name Can Not Be Empty.");
                        Console.ResetColor();

                        Console.WriteLine("Press Any Key To Try Again...");
                        Console.ReadKey();
                        Console.Clear();
                        continue;
                    }

                    authorName = char.ToUpper(authorName[0]) + authorName.Substring(1).ToLower();

                    name = authorName;
                    break;
                }

                string? surname;

                while (true)
                {
                    Console.Clear();

                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("Press Enter To Type Author Surname (Don't Have To) / Press 'M' To Back To Menu:");
                    Console.ResetColor();

                    string authorSurname = Console.ReadLine()?.Trim() ?? string.Empty;

                    if (authorSurname.ToLower() == "m")
                    {
                        Console.Clear();
                        return;
                    }

                    if (string.IsNullOrWhiteSpace(authorSurname))
                    {
                        surname = null;
                        break;
                    }

                    authorSurname = char.ToUpper(authorSurname[0]) + authorSurname.Substring(1).ToLower();

                    surname = authorSurname;
                    break;
                }

                Gender gender;

                while (true)
                {
                    Console.Clear();

                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("PLEASE CHOOSE AUTHOR GENDER");
                    Console.ResetColor();
                    Console.WriteLine();

                    foreach (Gender genders in Enum.GetValues(typeof(Gender)))
                    {
                        Console.WriteLine($"{(int)genders}. {genders}");
                    }

                    Console.WriteLine();
                    Console.Write("Enter Gender Number / Press 'M' To Back To Menu: ");

                    string genderNmbr = Console.ReadLine()?.Trim() ?? string.Empty;

                    if (genderNmbr.ToLower() == "m")
                    {
                        Console.Clear();
                        return;
                    }

                    if (!int.TryParse(genderNmbr, out int genderNumber))
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Library Error: Gender Must Be Chosen By Number.");
                        Console.ResetColor();

                        Console.WriteLine("Press Any Key To Try Again...");
                        Console.ReadKey();
                        Console.Clear();
                        continue;
                    }

                    if (!Enum.IsDefined(typeof(Gender), genderNumber))
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Library Error: Invalid Gender Number.");
                        Console.ResetColor();

                        Console.WriteLine("Press Any Key To Try Again...");
                        Console.ReadKey();
                        Console.Clear();
                        continue;
                    }

                    gender = (Gender)genderNumber;
                    break;
                }

                Author author = new Author
                {
                    Name = name,
                    Surname = surname,
                    Gender = gender
                };

                _authorRepository.Add(author);
                _authorRepository.SaveChanges();

                bool createAgain = false;

                while (true)
                {
                    Console.Clear();
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("Congratulations: The Author Was Successfully Created:");
                    Console.ResetColor();
                    Console.WriteLine(" ");
                    Console.WriteLine($"ID: {author.Id} \nName: {author.Name} \nSurname: {author.Surname ?? ":)"} \nGender: {author.Gender}");
                    Console.WriteLine(" ");
                    Console.ResetColor();
                    Console.WriteLine($"1. Create One More Author\n2. Back To Menu");

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
                            Console.WriteLine();
                            Console.WriteLine("Library Error: Invalid Choice.");
                            Console.ResetColor();

                            Console.WriteLine();
                            Console.WriteLine("Press Any Key To Try Again...");
                            Console.ReadKey();
                            continue;
                    }

                    break;
                }

                if (createAgain)
                {
                    continue;
                }
            
            }
        }

        public void ShowAllAuthors()
        {
            Console.Clear();

            List<Author> authors = _authorRepository.GetAll();
            List<Book> books = _bookRepository.GetAll();

            if (authors.Count == 0)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Library Error: There Is No Author Yet.");
                Console.ResetColor();

                Console.WriteLine();
                Console.WriteLine("Press Any Key To Back To Menu...");
                Console.ReadKey();
                Console.Clear();
                return;
            }

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("============== ALL AUTHORS ==============");
            Console.ResetColor();
            Console.WriteLine();

            foreach (Author author in authors)
            {
                int booksCount = books.Count(book => book.AuthorId == author.Id);

                Console.WriteLine($"ID: {author.Id}");
                Console.WriteLine($"Name: {author.Name}");
                Console.WriteLine($"Books Count: {booksCount}");
                Console.WriteLine("-----------------------------------------");
            }

            while (true)
            {
                Console.WriteLine();
                Console.WriteLine("1. Show Author's Books");
                Console.WriteLine("2. Back To Menu");

                string choice = Console.ReadLine()?.Trim() ?? string.Empty;

                switch (choice)
                {
                    case "1":

                        while (true)
                        {
                            Console.Clear();

                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.WriteLine("Type Author ID / Press 'M' To Back:");
                            Console.ResetColor();

                            string authorId = Console.ReadLine()?.Trim() ?? string.Empty;

                            if (authorId.ToLower() == "m")
                            {
                                Console.Clear();
                                break;
                            }

                            if (!int.TryParse(authorId, out int id))
                            {
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine("Library Error: Author ID Must Be A Number.");
                                Console.ResetColor();

                                Console.WriteLine();
                                Console.WriteLine("Press Any Key To Try Again...");
                                Console.ReadKey();
                                continue;
                            }

                            ShowAuthorsBooks(id);
                            return;
                        }

                        Console.Clear();

                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine("============== ALL AUTHORS ==============");
                        Console.ResetColor();
                        Console.WriteLine();

                        foreach (Author author in authors)
                        {
                            int booksCount = books.Count(book => book.AuthorId == author.Id);

                            Console.WriteLine($"ID: {author.Id}");
                            Console.WriteLine($"Name: {author.Name}");
                            Console.WriteLine($"Books Count: {booksCount}");
                            Console.WriteLine("-----------------------------------------");
                        }

                        break;

                    case "2":
                        Console.Clear();
                        return;

                    default:
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine();
                        Console.WriteLine("Library Error: Invalid Choice.");
                        Console.ResetColor();

                        Console.WriteLine();
                        Console.WriteLine("Press Any Key To Try Again...");
                        Console.ReadKey();
                        Console.Clear();

                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine("============== ALL AUTHORS ==============");
                        Console.ResetColor();
                        Console.WriteLine();

                        foreach (Author author in authors)
                        {
                            int booksCount = books.Count(book => book.AuthorId == author.Id);

                            Console.WriteLine($"ID: {author.Id}");
                            Console.WriteLine($"Name: {author.Name}");
                            Console.WriteLine($"Books Count: {booksCount}");
                            Console.WriteLine("-----------------------------------------");
                        }

                        break;
                }
            }
        }

        public void ShowAuthorsBooks(int authorId)
        {
            Console.Clear();

            List<Author> authors = _authorRepository.GetAll();
            List<Book> books = _bookRepository.GetAll();

            Author? author = authors.FirstOrDefault(a => a.Id == authorId);

            if (author is null)
            {
                while (true)
                {
                    Console.Clear();

                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Library Error: Author Not Found.");
                    Console.ResetColor();

                    Console.WriteLine();
                    Console.WriteLine("1. Try Again");
                    Console.WriteLine("2. Back To Menu");

                    string choice = Console.ReadLine()?.Trim() ?? string.Empty;

                    switch (choice)
                    {
                        case "1":

                            Console.Clear();

                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.WriteLine("============== ALL AUTHORS ==============");
                            Console.ResetColor();
                            Console.WriteLine();

                            foreach (Author item in authors)
                            {
                                int booksCount = books.Count(book => book.AuthorId == item.Id);

                                Console.WriteLine($"ID: {item.Id}");
                                Console.WriteLine($"Name: {item.Name}");
                                Console.WriteLine($"Books Count: {booksCount}");
                                Console.WriteLine("-----------------------------------------");
                            }

                            Console.WriteLine();
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.Write("Enter Author ID: ");
                            Console.ResetColor();

                            string idInput = Console.ReadLine()?.Trim() ?? string.Empty;

                            if (!int.TryParse(idInput, out int newAuthorId))
                            {
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine();
                                Console.WriteLine("Library Error: Author ID Must Be A Number.");
                                Console.ResetColor();

                                Console.WriteLine();
                                Console.WriteLine("Press Any Key To Try Again...");
                                Console.ReadKey();
                                continue;
                            }

                            ShowAuthorsBooks(newAuthorId);
                            return;

                        case "2":
                            Console.Clear();
                            return;

                        default:

                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine();
                            Console.WriteLine("Library Error: Invalid Choice.");
                            Console.ResetColor();

                            Console.WriteLine();
                            Console.WriteLine("Press Any Key To Try Again...");
                            Console.ReadKey();
                            break;
                    }
                }
            }

            List<Book> authorBooks = books
                .Where(book => book.AuthorId == author.Id)
                .ToList();

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"=========== {author.Name}'S BOOKS ===========");
            Console.ResetColor();
            Console.WriteLine();

            if (authorBooks.Count == 0)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("This Author Has No Books Yet.");
                Console.ResetColor();

                Console.WriteLine();
                Console.WriteLine("Press Any Key To Back To Menu...");
                Console.ReadKey();
                Console.Clear();
                return;
            }
            else
            {
                foreach (Book book in authorBooks)
                {
                    Console.WriteLine($"ID: {book.Id}");
                    Console.WriteLine($"Name: {book.Name}");
                    Console.WriteLine($"Page Count: {book.PageCount}");
                    Console.WriteLine($"Page Count: {book.ReservationStatus}");
                    Console.WriteLine("-----------------------------------------");
                }
            }

            Console.WriteLine();
            Console.WriteLine("Press Any Key To Back...");
            Console.ReadKey();
            Console.Clear();
        }
    }
}
