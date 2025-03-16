namespace ChildrensLeisure.DataAccess.Entity
{
    public class OrderFairyCharacter
    {
        public Guid OrderId { get; set; }
        public Order Order { get; set; }
        public Guid FairyCharacterId { get; set; }
        public FairyCharacter FairyCharacter { get; set; }
    }
}