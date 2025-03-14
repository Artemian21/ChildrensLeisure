using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChildrensLeisure.DataAccess.Entity
{
    public class Attraction
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public Guid ZoneId { get; set; }
        public Zone Zone { get; set; }
    }
}
