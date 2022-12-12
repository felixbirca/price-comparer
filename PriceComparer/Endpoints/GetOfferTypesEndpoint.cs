using FastEndpoints;
using PriceComparer.Application;
using PriceComparer.Application.DTOs.OfferType;
using PriceComparer.Domain;

namespace PriceComparer.Endpoints
{
    public class GetOfferTypesEndpoint : Endpoint<GetOfferTypesList, List<OfferType>>
    {
        private readonly IOfferTypesService _offerTypesService;

        public GetOfferTypesEndpoint(IOfferTypesService offerTypesService)
        {
            _offerTypesService = offerTypesService;
        }

        public override void Configure()
        {
            Get("/api/offer-types");
        }

        public override async Task HandleAsync(GetOfferTypesList req, CancellationToken cancellationToken)
        {
            await SendAsync((await _offerTypesService.GetList(req, cancellationToken)).ToList());
        }
    }
}
