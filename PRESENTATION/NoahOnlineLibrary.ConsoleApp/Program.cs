using NoahOnlineLibrary.Application.Interfaces.IRepository;
using NoahOnlineLibrary.Application.Interfaces.IServices;
using NoahOnlineLibrary.Application.Services;
using NoahOnlineLibrary.Persistence.DAL;
using NoahOnlineLibrary.Persistence.Repository;

namespace NoahOnlineLibrary.ConsoleApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var context = new AppDbContext();

            var authorRepo = new AuthorRepository(context);
            var bookRepo = new BookRepository(context);

            var bookService = new BookService(bookRepo, authorRepo);
            var authorService = new AuthorService(authorRepo, bookRepo);

            authorService.ShowAllAuthors();

            //bookService.GetBookById();
            
        }
    }
}
