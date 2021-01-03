using BookMyEvent.Services.EventCatalog.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BookMyEvent.Services.EventCatalog.Repositories
{
    public interface IEventRepository
    {
        Task<IEnumerable<Event>> GetEvents(Guid categoryId);
        Task<Event> GetEventById(Guid eventId);
        Task<bool> SaveChanges();
    }
}
