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
    public class OrderZoneConfiguration : IEntityTypeConfiguration<OrderZone>
    {
        public void Configure(EntityTypeBuilder<OrderZone> builder)
        {
            builder.HasKey(oz => new { oz.OrderId, oz.ZoneId });

            builder.HasOne(oz => oz.Order)
                   .WithMany(o => o.OrderZones)
                   .HasForeignKey(oz => oz.OrderId);

            builder.HasOne(oz => oz.Zone)
                   .WithMany()
                   .HasForeignKey(oz => oz.ZoneId);
        }
    }
}
