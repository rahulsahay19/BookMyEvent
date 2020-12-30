using BookMyEvent.Services.Marketing.Entities;
using Microsoft.EntityFrameworkCore;

namespace BookMyEvent.Services.Marketing.DbContexts
{
    public class MarketingDbContext : DbContext
    {
        public MarketingDbContext(DbContextOptions<MarketingDbContext> options)
            : base(options)
        {
        }

        public DbSet<BasketChangeEvent> BasketChangeEvents { get; set; }
    }
}
