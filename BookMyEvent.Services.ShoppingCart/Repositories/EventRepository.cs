using BookMyEvent.Services.ShoppingCart.DbContexts;
using BookMyEvent.Services.ShoppingCart.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace BookMyEvent.Services.ShoppingCart.Repositories
{
    public class EventRepository : IEventRepository
    {
        private readonly ShoppingCartDbContext _shoppingCartDbContext;

        public EventRepository(ShoppingCartDbContext shoppingCartDbContext)
        {
            _shoppingCartDbContext = shoppingCartDbContext;
        }

        public async Task<bool> EventExists(Guid eventId)
        {
            return await _shoppingCartDbContext.Events.AnyAsync(e => e.EventId == eventId);
        }

        public void AddEvent(Event theEvent)
        {
            _shoppingCartDbContext.Events.Add(theEvent);

        }

        public async Task<bool> SaveChanges()
        {
            return (await _shoppingCartDbContext.SaveChangesAsync() > 0);
        }
    }
}
