using Microsoft.EntityFrameworkCore;
using PriceComparer.Application.DTOs.OfferType;
using PriceComparer.Domain;
using PriceComparer.Infrastucture;

namespace PriceComparer.Application
{
    public class OfferTypesService : IOfferTypesService
    {
        private PriceComparerContext _context;

        public OfferTypesService(PriceComparerContext context)
        {
            _context = context;
        }

        public async Task Create(CreateOfferType request, CancellationToken cancellationToken)
        {
            await _context.AddAsync(new OfferType { Name = request.Name, UserId = request.UserId }, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task<IEnumerable<OfferType>> GetList(GetOfferTypesList request, CancellationToken cancellationToken)
        {
            var list = _context.OfferTypes
                .OrderBy(x => x.Name)
                .AsQueryable();

            if (!string.IsNullOrEmpty(request.Search))
            {
                list = list.Where(x => x.Name == request.Search);
            }

            if (request.PageSize.HasValue && request.Page.HasValue)
            {
                list = list.Skip(request.Page.Value * request.PageSize.Value);
            }

            return await list.ToListAsync(cancellationToken);
        }

        public async Task<bool> TypeExists(string typeName, CancellationToken cancellationToken)
        {
            return await _context.OfferTypes.AnyAsync(x => x.Name == typeName);
        }
    }
}
