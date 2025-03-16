using ChildrensLeisure.DataAccess.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChildrensLeisure.DataAccess.Config
{
    public class AttractionConfiguration : IEntityTypeConfiguration<Attraction>
    {
        public void Configure(EntityTypeBuilder<Attraction> builder)
        {
            builder.HasKey(a => a.Id);
            builder.Property(a => a.Name).IsRequired().HasMaxLength(100);
            builder.Property(a => a.Price).IsRequired().HasColumnType("decimal(18,2)");

            builder.HasOne(a => a.Zone)
                   .WithMany(z => z.Attractions)
                   .HasForeignKey(a => a.ZoneId)
                   .OnDelete(DeleteBehavior.SetNull);
        }
    }
}
