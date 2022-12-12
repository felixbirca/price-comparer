using FastEndpoints;
using FluentValidation;
using System.Security.Claims;

namespace PriceComparer.Application.DTOs.Offer
{
    public class CreateOffer
    {
        [FromClaim(ClaimType = ClaimTypes.NameIdentifier)]
        public string UserId { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public IEnumerable<int> OfferTypesIds { get; set; } = new List<int>();
    }

    public class CreateOfferValidator : Validator<CreateOffer>
    {
        public CreateOfferValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .WithMessage("Name is required");
        }
    }
}
