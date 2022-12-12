using FastEndpoints;
using FluentValidation;
using System.Security.Claims;

namespace PriceComparer.Application.DTOs.OfferType
{
    public class CreateOfferType
    {
        [FromClaim(ClaimType = ClaimTypes.NameIdentifier)]
        public string UserId { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
    }

    public class CreateOfferTypeValidator : Validator<CreateOfferType>
    {
        public CreateOfferTypeValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .WithMessage("Name is required;");
        }
    }
}
