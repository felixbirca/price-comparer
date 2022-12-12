namespace PriceComparer.Domain
{
    public class OfferType : BaseEntity
    {
        public string UserId { get; set; }
        public string Name { get; set; }
        public ICollection<Offer> Services { get; set; }
    }
}
