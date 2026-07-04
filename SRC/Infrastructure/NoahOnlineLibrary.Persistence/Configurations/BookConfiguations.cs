using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NoahOnlineLibrary.Domain.Entities;
using NoahOnlineLibrary.Domain.Enums;
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
                   .HasColumnType("NVARCHAR(100)");

            builder.Property(b => b.PageCount)
                   .IsRequired();

            builder.HasOne(b => b.Author)
                   .WithMany(a => a.Books)
                   .HasForeignKey(b => b.AuthorId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(b => b.ReservedItems)
                   .WithOne(r => r.Book)
                   .HasForeignKey(r => r.BookId);

            builder.ToTable(b =>
            {
                b.HasCheckConstraint(
                    "CK_Book_ReservationStatus",
                    "[ReservationStatus] IN (1,2,3,4,5)");

                b.HasCheckConstraint(
                    "CK_Book_PageCount",
                    "[PageCount] >= 0");
            });
        }
    }
}
