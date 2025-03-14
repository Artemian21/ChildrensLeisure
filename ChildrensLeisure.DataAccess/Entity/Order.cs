using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChildrensLeisure.DataAccess.Entity
{
    public class Order
    {
        public Guid Id { get; set; }
        public DateTime OrderDate { get; set; }
        public decimal TotalPrice { get; set; }
        public List<Zone> Zones { get; set; }
        public List<Attraction> Attractions { get; set; }
        public List<FairyCharacter> FairyCharacters { get; set; }
        public Guid EventProgramId { get; set; }
        public EventProgram EventProgram { get; set; }
    }
}
