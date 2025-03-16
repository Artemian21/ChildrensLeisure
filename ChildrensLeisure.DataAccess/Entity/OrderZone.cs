namespace ChildrensLeisure.DataAccess.Entity
{
    public class OrderZone
    {
        public Guid OrderId { get; set; }
        public Order Order { get; set; }
        public Guid ZoneId { get; set; }
        public Zone Zone { get; set; }
    }
}