using FastEndpoints;

namespace PriceComparer.Application.DTOs.OfferType
{
    public class GetOfferTypesList
    {
        public string? Search { get; set; }
        public int? Page { get; set; } = 0;
        public int? PageSize { get; set; } = 10;
    }
}
