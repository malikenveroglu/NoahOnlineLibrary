using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoahOnlineLibrary.Application.Interfaces.IServices
{
    public interface IAuthorService
    {
        void CreateAuthor();
        void ShowAllAuthors();
        void ShowAuthorsBooks(int authorId);
    }
}
