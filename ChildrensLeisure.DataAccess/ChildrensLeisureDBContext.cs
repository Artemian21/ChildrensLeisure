using ChildrensLeisure.DataAccess.Entity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ChildrensLeisure.DataAccess
{
    public class ChildrensLeisureDBContext : DbContext
    {
        public ChildrensLeisureDBContext(DbContextOptions<ChildrensLeisureDBContext> options) : base(options)
        { }

        public DbSet<Zone> Zones { get; set; }
        public DbSet<Attraction> Attractions { get; set; }
        public DbSet<FairyCharacter> FairyCharacters { get; set; }
        public DbSet<EventProgram> EventPrograms { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderAttraction> OrderAttractions { get; set; }
        public DbSet<OrderFairyCharacter> OrderFairyCharacters { get; set; }
        public DbSet<OrderZone> OrderZones { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            base.OnModelCreating(modelBuilder);
        }
    }
}
