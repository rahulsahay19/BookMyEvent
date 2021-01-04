using BookMyEvent.Web.Bff.Models.Api;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BookMyEvent.Web.Bff.Services
{
    public interface IEventCatalogService
    {
        Task<IEnumerable<Event>> GetAll();
        Task<IEnumerable<Event>> GetByCategoryId(Guid categoryid);
        Task<Event> GetEvent(Guid id);
        Task<IEnumerable<Category>> GetCategories();
        Task<PriceUpdate> UpdatePrice(PriceUpdate priceUpdate);

        Task<CatalogBrowse> GetCatalogBrowse(Guid basketId, Guid categoryId);
    }
}
