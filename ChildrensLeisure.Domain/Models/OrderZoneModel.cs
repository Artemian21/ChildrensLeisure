using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChildrensLeisure.Domain.Models
{
    public class OrderZoneModel
    {
        public Guid OrderId { get; set; }
        public OrderModel Order { get; set; }

        public Guid ZoneId { get; set; }
        public OrderModel Zone { get; set; }
    }
}
