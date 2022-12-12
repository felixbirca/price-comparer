namespace PriceComparer.Domain
{
    public class Review : BaseEntity
    {
        public string Content { get; set; }
        public Offer Service { get; set; }
        public int ServiceId { get; set; }
    }
}
