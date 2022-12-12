namespace PriceComparer.Domain
{
    public class Review : BaseEntity
    {
        public string Content { get; set; } = string.Empty;
        public Offer Service { get; set; } = null!;
        public int ServiceId { get; set; }
    }
}
