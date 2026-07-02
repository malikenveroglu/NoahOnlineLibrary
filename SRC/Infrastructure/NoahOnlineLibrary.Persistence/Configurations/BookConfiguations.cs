using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NoahOnlineLibrary.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoahOnlineLibrary.Persistence.Cofigurations
{
    internal class BookConfiguations : IEntityTypeConfiguration<Book>
    {
        public void Configure(EntityTypeBuilder<Book> builder)
        {
            builder.Property(b => b.Name)
                   .IsRequired()
                   .HasMaxLength(100);

            builder.Property(b => b.PageCount)
                   .IsRequired();

            builder.HasMany(b => b.ReservedItems)
                   .WithOne(r => r.Book)
                   .HasForeignKey(r => r.BookId);

            builder.ToTable(b =>
            {
                b.HasCheckConstraint("CK_Book_PageCount", "PageCount > 0");
            });
        }
    }
}
