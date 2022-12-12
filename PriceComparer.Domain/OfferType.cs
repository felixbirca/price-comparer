namespace PriceComparer.Domain
{
    public class OfferType : BaseEntity
    {
        public string UserId { get; set; } = string.Empty; 
        public string Name { get; set; } = string.Empty;
        public ICollection<Offer> Services { get; set; } = new List<Offer>();
    }
}
