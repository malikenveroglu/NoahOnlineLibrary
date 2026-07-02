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
    internal class ReservedItemConfigurations : IEntityTypeConfiguration<ReservedItem>
    {
        public void Configure(EntityTypeBuilder<ReservedItem> builder)
        {
            builder.Property(r => r.FinCode)
                   .IsRequired()
                   .HasMaxLength(7);

            builder.Property(r => r.StartDate)
                   .IsRequired();

            builder.Property(r => r.EndDate)
                   .IsRequired();

            builder.Property(r => r.Status)
                   .IsRequired();

            builder.HasIndex(r => r.FinCode);

            builder.ToTable(r =>
            {
                r.HasCheckConstraint("CK_ReservedItem_DateRange",
                                     "[StartDate] <= [EndDate]");
            });
        }
    }
}
