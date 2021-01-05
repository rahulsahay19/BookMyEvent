using BookMyEvent.Web.Bff.Extensions;
using BookMyEvent.Web.Bff.Models.Api;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace BookMyEvent.Web.Bff.Services
{
    public class EventCatalogService : IEventCatalogService
    {
        private readonly HttpClient client;

        public EventCatalogService(HttpClient client)
        {
            this.client = client;
        }

        public async Task<IEnumerable<Event>> GetAll()
        {
            var response = await client.GetAsync("/api/bffweb/events");
            return await response.ReadContentAs<List<Event>>();
        }

        public async Task<IEnumerable<Event>> GetByCategoryId(Guid categoryid)
        {
            var response = await client.GetAsync($"/api/bffweb/events/?categoryId={categoryid}");
            return await response.ReadContentAs<List<Event>>();
        }

        public async Task<Event> GetEvent(Guid id)
        {
            var response = await client.GetAsync($"/api/bffweb/events/{id}");
            return await response.ReadContentAs<Event>();
        }

        public async Task<IEnumerable<Category>> GetCategories()
        {
            var response = await client.GetAsync("/api/bffweb/categories");
            return await response.ReadContentAs<List<Category>>();
        }

        public async Task<PriceUpdate> UpdatePrice(PriceUpdate priceUpdate)
        {
            var response = await client.PostAsJson($"api/bffweb/events/eventpriceupdate", priceUpdate);
            return await response.ReadContentAs<PriceUpdate>();
        }

        //Calls optimized for use through the gateway

        #region Gateway calls

        public async Task<CatalogBrowse> GetCatalogBrowse(Guid basketId, Guid categoryId)
        {
            var response = await client.GetAsync($"/api/bffweb/events/catalogbrowse/{categoryId}/basket/{basketId}");
            return await response.ReadContentAs<CatalogBrowse>();
        }

        #endregion
    }

}


