using BookMyEvent.Services.ShoppingCart.Entities;
using System;
using System.Threading.Tasks;

namespace BookMyEvent.Services.ShoppingCart.Repositories
{
    public interface IEventRepository
    {
        void AddEvent(Event theEvent);
        Task<bool> EventExists(Guid eventId);
        Task<bool> SaveChanges();
    }
}
