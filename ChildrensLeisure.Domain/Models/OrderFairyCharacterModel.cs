using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChildrensLeisure.Domain.Models
{
    public class OrderFairyCharacterModel
    {
        public Guid OrderId { get; set; }
        public OrderModel Order { get; set; }

        public Guid FairyCharacterId { get; set; }
        public FairyCharacterModel FairyCharacter { get; set; }
    }
}
