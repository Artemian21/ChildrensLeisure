using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChildrensLeisure.Domain.Models
{
    public class OrderModel
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public DateTime OrderDate { get; set; } = DateTime.UtcNow;
        public decimal TotalPrice { get; set; }
        public List<OrderZoneModel>? OrderZones { get; set; } = new();
        public List<OrderAttractionModel>? OrderAttractions { get; set; } = new();
        public List<OrderFairyCharacterModel>? OrderFairyCharacters { get; set; } = new();
        public Guid? EventProgramId { get; set; }
        public EventProgramModel? EventProgram { get; set; }
    }
}
