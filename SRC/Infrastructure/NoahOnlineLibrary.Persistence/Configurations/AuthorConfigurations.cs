using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NoahOnlineLibrary.Domain.Entities;
using NoahOnlineLibrary.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoahOnlineLibrary.Persistence.Configurations
{
    internal class AuthorConfigurations : IEntityTypeConfiguration<Author>
    {
        public void Configure(EntityTypeBuilder<Author> builder)
        {
            builder.Property(a => a.Name)
                   .IsRequired()
                   .HasColumnType("nvarchar(100)");

            builder.Property(a => a.Surname)
                   .HasColumnType("nvarchar(100)")
                   .HasDefaultValue(":)");

            builder.Property(a => a.Gender)
                   .HasDefaultValue(Gender.Unknown);

            builder.ToTable(a =>
            {
                a.HasCheckConstraint(
                    "CK_Author_Gender",
                    "[Gender] IN (1,2,3,4)");
            });
        }
    }
}
