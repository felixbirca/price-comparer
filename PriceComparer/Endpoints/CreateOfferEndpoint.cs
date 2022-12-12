using FastEndpoints;
using PriceComparer.Application;
using PriceComparer.Application.DTOs.Offer;

namespace PriceComparer.Endpoints
{
    public class CreateOfferEndpoint : Endpoint<CreateOffer>
    {
        private readonly IOffersService _offersService;

        public CreateOfferEndpoint(IOffersService offersService)
        {
            _offersService = offersService;
        }

        public override void Configure()
        {
            AllowAnonymous();
            Post("/api/offer/create");
        }

        public override async Task HandleAsync(CreateOffer request, CancellationToken cancellationToken)
        {
            ThrowIfAnyErrors();
            await _offersService.Create(request, cancellationToken);
            await SendOkAsync();
        }
    }
}
