using NoahOnlineLibrary.Application.Interfaces.IRepository;
using NoahOnlineLibrary.Application.Interfaces.IServices;
using NoahOnlineLibrary.Application.Services;
using NoahOnlineLibrary.Persistence.DAL;
using NoahOnlineLibrary.Persistence.Repository;

namespace NoahOnlineLibrary.ConsoleApp
{
    internal class Program
    {
        //private static IReservedItemService reservedItemService;
        //private static IBookService bookService;
        //private static IAuthorService authorService;

        static void Main(string[] args)
        {
                AppDbContext context = new AppDbContext();

                IBookRepository bookRepository = new BookRepository(context);
                IAuthorRepository authorRepository = new AuthorRepository(context);
                IReservedItemRepository reservedItemRepository = new ReservedItemRepository(context);

                IBookService bookService = new BookService(bookRepository, authorRepository, reservedItemRepository);
                IAuthorService authorService = new AuthorService(authorRepository, bookRepository);
                IReservedItemService reservedItemService = new ReservedItemService(reservedItemRepository, bookRepository, authorRepository);

                LibraryManagementApp app = new LibraryManagementApp(bookService, authorService, reservedItemService);

                app.Run();
        }
    }
}
