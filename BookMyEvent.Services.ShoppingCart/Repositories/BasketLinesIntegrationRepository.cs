using BookMyEvent.Services.ShoppingCart.DbContexts;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace BookMyEvent.Services.ShoppingCart.Repositories
{
    public class BasketLinesIntegrationRepository : IBasketLinesIntegrationRepository
    {
        private readonly DbContextOptions<ShoppingCartDbContext> dbContextOptions;


        public BasketLinesIntegrationRepository(DbContextOptions<ShoppingCartDbContext> dbContextOptions)
        {
            this.dbContextOptions = dbContextOptions;
        }

        public async Task UpdatePricesForIntegrationEvent(DTOs.PriceUpdate priceUpdate)
        {
            await using (var shoppingBasketDbContext = new ShoppingCartDbContext(dbContextOptions))
            {
                var basketLinesToUpdate = shoppingBasketDbContext.BasketLines.Where(x => x.EventId == priceUpdate.EventId);

                await basketLinesToUpdate.ForEachAsync((basketLineToUpdate) =>
                    basketLineToUpdate.Price = priceUpdate.Price
                );
                await shoppingBasketDbContext.SaveChangesAsync();
            }
        }
    }
}
