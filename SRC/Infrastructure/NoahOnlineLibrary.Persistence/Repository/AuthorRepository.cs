using NoahOnlineLibrary.Application.Interfaces.IRepository;
using NoahOnlineLibrary.Domain.Entities;
using NoahOnlineLibrary.Persistence.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoahOnlineLibrary.Persistence.Repository
{
    public class AuthorRepository: Repository<Author>, IAuthorRepository
    {
        public AuthorRepository(AppDbContext context) : base(context) { }
    }
}
