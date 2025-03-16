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

        public List<OrderZone>? OrderZones { get; set; } = new();
        public List<OrderAttraction>? OrderAttractions { get; set; } = new();
        public List<OrderFairyCharacter>? OrderFairyCharacters { get; set; } = new();

        public Guid? EventProgramId { get; set; }
        public EventProgram? EventProgram { get; set; }
    }
}
