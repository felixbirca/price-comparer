using PriceComparer.Application.DTOs.OfferType;
using PriceComparer.Domain;

namespace PriceComparer.Application
{
    public interface IOfferTypesService
    {
        Task Create(CreateOfferType request, CancellationToken cancellationToken);
        Task<bool> TypeExists(string typeName, CancellationToken cancellationToken);
        Task<IEnumerable<OfferType>> GetList(GetOfferTypesList request, CancellationToken cancellationToken);
    }
}