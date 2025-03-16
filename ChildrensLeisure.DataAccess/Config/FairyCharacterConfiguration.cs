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
    public class FairyCharacterConfiguration : IEntityTypeConfiguration<FairyCharacter>
    {
        public void Configure(EntityTypeBuilder<FairyCharacter> builder)
        {
            builder.HasKey(fc => fc.Id);
            builder.Property(fc => fc.Name).IsRequired().HasMaxLength(100);
            builder.Property(fc => fc.Price).IsRequired().HasColumnType("decimal(18,2)");
        }
    }
}
