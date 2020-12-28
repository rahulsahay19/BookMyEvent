using BookMyEvent.Services.ShoppingCart.DbContexts;
using BookMyEvent.Services.ShoppingCart.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookMyEvent.Services.ShoppingCart.Repositories
{
    public class BasketLinesRepository : IBasketLinesRepository
    {
        private readonly ShoppingCartDbContext _shoppingCartDbContext;

        public BasketLinesRepository(ShoppingCartDbContext shoppingCartDbContext)
        {
            _shoppingCartDbContext = shoppingCartDbContext;
        }

        public async Task<IEnumerable<BasketLine>> GetBasketLines(Guid basketId)
        {
            return await _shoppingCartDbContext.BasketLines.Include(bl => bl.Event)
                .Where(b => b.BasketId == basketId).ToListAsync();
        }

        public async Task<BasketLine> GetBasketLineById(Guid basketLineId)
        {
            return await _shoppingCartDbContext.BasketLines.Include(bl => bl.Event)
                .Where(b => b.BasketLineId == basketLineId).FirstOrDefaultAsync();
        }

        public async Task<BasketLine> AddOrUpdateBasketLine(Guid basketId, BasketLine basketLine)
        {
            var existingLine = await _shoppingCartDbContext.BasketLines.Include(bl => bl.Event)
                .Where(b => b.BasketId == basketId && b.EventId == basketLine.EventId).FirstOrDefaultAsync();
            if (existingLine == null)
            {
                basketLine.BasketId = basketId;
                _shoppingCartDbContext.BasketLines.Add(basketLine);
                return basketLine;
            }
            existingLine.TicketAmount += basketLine.TicketAmount;
            return existingLine;
        }

        public void UpdateBasketLine(BasketLine basketLine)
        {
            // empty on purpose
        }

        public void RemoveBasketLine(BasketLine basketLine)
        {
            _shoppingCartDbContext.BasketLines.Remove(basketLine);
        }

        public async Task<bool> SaveChanges()
        {
            return (await _shoppingCartDbContext.SaveChangesAsync() > 0);
        }
    }
}
