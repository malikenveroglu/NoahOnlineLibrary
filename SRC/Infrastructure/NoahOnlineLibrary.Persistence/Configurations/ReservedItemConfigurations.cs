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
    internal class ReservedItemConfigurations : IEntityTypeConfiguration<ReservedItem>
    {
        public void Configure(EntityTypeBuilder<ReservedItem> builder)
        {
            builder.Property(r => r.FinCode)
                   .IsRequired()
                   .HasColumnType("CHAR(7)");

            builder.Property(r => r.StartDate)
                   .IsRequired()
                   .HasColumnType("Date");

            builder.Property(r => r.EndDate)
                   .IsRequired()
                   .HasColumnType("Date");

            builder.Property(r => r.Status)
                   .IsRequired()
                   .HasDefaultValue(Status.Confirmed);

            builder.HasIndex(r => r.FinCode);

            builder.ToTable(r =>
            {
                r.HasCheckConstraint("CK_ReservedItem_DateRange",
                                     "[StartDate] <= [EndDate]");
            });
        }
    }
}
