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
        private readonly ShoppingCartDbContext _shoppingCartDbContext;

        public BasketRepository(ShoppingCartDbContext shoppingCartDbContext)
        {
            _shoppingCartDbContext = shoppingCartDbContext;
        }

        public async Task<Basket> GetBasketById(Guid basketId)
        {
            return await _shoppingCartDbContext.Baskets.Include(sb => sb.BasketLines)
                .Where(b => b.BasketId == basketId).FirstOrDefaultAsync();
        }

        public async Task<bool> BasketExists(Guid basketId)
        {
            return await _shoppingCartDbContext.Baskets
                .AnyAsync(b => b.BasketId == basketId);
        }

        public async Task ClearBasket(Guid basketId)
        {
            var basketLinesToClear = _shoppingCartDbContext.BasketLines.Where(b => b.BasketId == basketId);
            _shoppingCartDbContext.BasketLines.RemoveRange(basketLinesToClear);

            var basket = _shoppingCartDbContext.Baskets.FirstOrDefault(b => b.BasketId == basketId);
            if (basket != null) basket.CouponId = null;

            await SaveChanges();
        }

        public void AddBasket(Basket basket)
        {
            _shoppingCartDbContext.Baskets.Add(basket);
        }

        public async Task<bool> SaveChanges()
        {
            return (await _shoppingCartDbContext.SaveChangesAsync() > 0);
        }
    }
}

