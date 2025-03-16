using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChildrensLeisure.Domain.Models
{
    public class OrderAttractionModel
    {
        public Guid OrderId { get; set; }
        public OrderModel Order { get; set; }
        public Guid AttractionId { get; set; }
        public AttractionModel Attraction { get; set; }

    }
}
