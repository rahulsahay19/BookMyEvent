using BookMyEvent.Services.ShoppingCart.Entities;
using System;
using System.Threading.Tasks;

namespace BookMyEvent.Services.ShoppingCart.Services
{
    public interface IEventCatalogService
    {
        Task<Event> GetEvent(Guid id);
    }
}
