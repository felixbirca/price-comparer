using Microsoft.EntityFrameworkCore;
using PriceComparer.Application.DTOs.Offer;
using PriceComparer.Domain;
using PriceComparer.Infrastucture;

namespace PriceComparer.Application
{
    public class OffersService : IOffersService
    {
        private PriceComparerContext _context;

        public OffersService(PriceComparerContext context)
        {
            _context = context;
        }

        public async Task Create(CreateOffer request, CancellationToken cancellationToken)
        {
            await _context.AddAsync(new Offer
            {
                Name = request.Name,
                Address = request.Address,
                UserId = request.UserId,
                OfferTypes = await _context.OfferTypes
                                           .Where(x => request.OfferTypesIds.Contains(x.Id))
                                           .ToListAsync(cancellationToken)
            }, cancellationToken);
            await _context.SaveChangesAsync();
        }
    }
}
