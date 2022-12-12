using FastEndpoints;
using System.Security.Claims;

namespace PriceComparer.Application.DTOs.Offer
{
    public class CreateOffer
    {
        [FromClaim(ClaimType = ClaimTypes.NameIdentifier)]
        public string UserId { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public IEnumerable<int> OfferTypesIds { get; set; }
    }
}
