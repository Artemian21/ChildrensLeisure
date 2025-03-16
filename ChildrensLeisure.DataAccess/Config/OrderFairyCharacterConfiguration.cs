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
    public class OrderFairyCharacterConfiguration : IEntityTypeConfiguration<OrderFairyCharacter>
    {
        public void Configure(EntityTypeBuilder<OrderFairyCharacter> builder)
        {
            builder.HasKey(ofc => new { ofc.OrderId, ofc.FairyCharacterId });

            builder.HasOne(ofc => ofc.Order)
                   .WithMany(o => o.OrderFairyCharacters)
                   .HasForeignKey(ofc => ofc.OrderId);

            builder.HasOne(ofc => ofc.FairyCharacter)
                   .WithMany()
                   .HasForeignKey(ofc => ofc.FairyCharacterId);
        }
    }
}
