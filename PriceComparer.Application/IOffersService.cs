using PriceComparer.Application.DTOs.Offer;

namespace PriceComparer.Application
{
    public interface IOffersService
    {
        Task Create(CreateOffer request, CancellationToken cancellationToken);
    }
}