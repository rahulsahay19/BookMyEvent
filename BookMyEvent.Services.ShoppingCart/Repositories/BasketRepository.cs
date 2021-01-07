using BookMyEvent.Services.ShoppingCart.DbContexts;
using BookMyEvent.Services.ShoppingCart.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace BookMyEvent.Services.ShoppingCart.Repositories
{
    public class BasketRepository : IBasketRepository
    {
        private readonly ShoppingCartDbContext shoppingBasketDbContext;

        public BasketRepository(ShoppingCartDbContext shoppingBasketDbContext)
        {
            this.shoppingBasketDbContext = shoppingBasketDbContext;
        }

        public async Task<Basket> GetBasketById(Guid basketId)
        {
            return await shoppingBasketDbContext.Baskets.Include(sb => sb.BasketLines)
                .Where(b => b.BasketId == basketId).FirstOrDefaultAsync();
        }

        public async Task<bool> BasketExists(Guid basketId)
        {
            return await shoppingBasketDbContext.Baskets
                .AnyAsync(b => b.BasketId == basketId);
        }

        public async Task ClearBasket(Guid basketId)
        {
            var basketLinesToClear = shoppingBasketDbContext.BasketLines.Where(b => b.BasketId == basketId);
            shoppingBasketDbContext.BasketLines.RemoveRange(basketLinesToClear);

            var basket = shoppingBasketDbContext.Baskets.FirstOrDefault(b => b.BasketId == basketId);

            await SaveChanges();
        }

        public void AddBasket(Basket basket)
        {
            shoppingBasketDbContext.Baskets.Add(basket);
        }

        public async Task<bool> SaveChanges()
        {
            return (await shoppingBasketDbContext.SaveChangesAsync() > 0);
        }
    }
}
