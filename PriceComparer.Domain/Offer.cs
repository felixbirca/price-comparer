namespace PriceComparer.Domain
{
    public class Offer : BaseEntity
    {
        public string UserId { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public ICollection<OfferType> OfferTypes { get; set; }
    }
}