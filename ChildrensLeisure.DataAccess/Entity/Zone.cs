using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChildrensLeisure.DataAccess.Entity
{
    public class Zone
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public List<Attraction> Attractions { get; set; }
    }
}
