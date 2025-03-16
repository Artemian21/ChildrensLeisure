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
    public class OrderAttractionConfiguration : IEntityTypeConfiguration<OrderAttraction>
    {
        public void Configure(EntityTypeBuilder<OrderAttraction> builder)
        {
            builder.HasKey(oa => new { oa.OrderId });

            builder.HasOne(oa => oa.Order)
                   .WithMany(o => o.OrderAttractions)
                   .HasForeignKey(oa => oa.OrderId);

            builder.HasOne(oa => oa.Attraction)
                   .WithMany()
                   .HasForeignKey(oa => oa.AttractionId);
        }
    }
}
