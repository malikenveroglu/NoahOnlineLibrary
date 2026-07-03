using Microsoft.EntityFrameworkCore;
using NoahOnlineLibrary.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoahOnlineLibrary.Persistence.DAL
{
    public class AppDbContext: DbContext
    {
        public AppDbContext(AppDbContext? context)
        {
            Context = context;
        }

        public AppDbContext()
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlServer("server=localhost;database=NoahOnlineLibraryTRAINING;trusted_connection=true;integrated security=true;trustservercertificate=true;");
        }
        public DbSet<Book> Books { get; set; }
        public DbSet<Author> Authors { get; set; }
        public DbSet<ReservedItem> ReservedItems { get; set; }
        public AppDbContext? Context { get; }
    }
}
