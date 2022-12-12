using PriceComparer.Application;
using PriceComparer.Application.DTOs.OfferType;

namespace PriceComparer.Endpoints
{
    public class CreateOfferTypeEndpoint : Endpoint<CreateOfferType>
    {
        private readonly IOfferTypesService _offerTypesService;

        public CreateOfferTypeEndpoint(IOfferTypesService offerTypesService)
        {
            _offerTypesService = offerTypesService;
        }

        public override void Configure()
        {
            Post("/api/offer-types/create");
        }

        public override async Task HandleAsync(CreateOfferType req, CancellationToken ct)
        {
            var typeExists = await _offerTypesService.TypeExists(req.Name, ct);

            if (typeExists)
                AddError(r => r.Name, "A type with this name already exists");

            ThrowIfAnyErrors();

            await _offerTypesService.Create(req, ct);
            await SendOkAsync();
        }
    }
}
