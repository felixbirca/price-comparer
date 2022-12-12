using Microsoft.EntityFrameworkCore;
using PriceComparer.Domain;

namespace PriceComparer.Infrastucture
{
    public class PriceComparerContext : DbContext
    {
        public PriceComparerContext(DbContextOptions<PriceComparerContext> options) : base(options)
        {
        }

        public DbSet<Offer> Offers { get; set; }
        public DbSet<OfferType> OfferTypes { get; set; }
        public DbSet<Review> Reviews { get; set; }
    }
}