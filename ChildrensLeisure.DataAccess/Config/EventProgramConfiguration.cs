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
    public class EventProgramConfiguration : IEntityTypeConfiguration<EventProgram>
    {
        public void Configure(EntityTypeBuilder<EventProgram> builder)
        {
            builder.HasKey(ep => ep.Id);
            builder.Property(ep => ep.Name)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(ep => ep.Price)
                .IsRequired()
                .HasColumnType("decimal(18,2)");

            builder.Property(ep => ep.IsCustomProgram)
                .IsRequired();
        }
    }
}
