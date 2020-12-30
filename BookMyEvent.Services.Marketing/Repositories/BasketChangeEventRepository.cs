using BookMyEvent.Services.Marketing.DbContexts;
using BookMyEvent.Services.Marketing.Entities;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace BookMyEvent.Services.Marketing.Repositories
{
    public class BasketChangeEventRepository : IBasketChangeEventRepository
    {
        private readonly DbContextOptions<MarketingDbContext> dbContextOptions;

        public BasketChangeEventRepository(DbContextOptions<MarketingDbContext> dbContextOptions)
        {
            this.dbContextOptions = dbContextOptions;
        }

        public async Task AddBasketChangeEvent(BasketChangeEvent basketChangeEvent)
        {
            await using (var marketingDbContext = new MarketingDbContext(dbContextOptions))
            {
                await marketingDbContext.BasketChangeEvents.AddAsync(basketChangeEvent);
                await marketingDbContext.SaveChangesAsync();
            }
        }
    }
}
