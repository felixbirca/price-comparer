namespace PriceComparer.Domain
{
    public class Offer : BaseEntity
    {
        public string UserId { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public ICollection<OfferType> OfferTypes { get; set; } = new List<OfferType>();
    }
}