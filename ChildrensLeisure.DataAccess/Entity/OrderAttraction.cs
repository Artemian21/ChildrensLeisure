using System.ComponentModel.DataAnnotations;

namespace ChildrensLeisure.DataAccess.Entity
{
    public class OrderAttraction
    {
        public Guid OrderId { get; set; }
        public Order Order { get; set; }
        public Guid AttractionId { get; set; }
        public Attraction Attraction { get; set; }
    }
}