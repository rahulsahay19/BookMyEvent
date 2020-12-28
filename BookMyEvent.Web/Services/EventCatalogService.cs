using BookMyEvent.Web.Extensions;
using BookMyEvent.Web.Models.Api;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace BookMyEvent.Web.Services
{
    public class EventCatalogService : IEventCatalogService
    {
        private readonly HttpClient _httpClient;

        public EventCatalogService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<IEnumerable<Event>> GetAll()
        {
            var response = await _httpClient.GetAsync("/api/events");
            return await response.ReadContentAs<List<Event>>();
        }

        public async Task<IEnumerable<Event>> GetByCategoryId(Guid categoryid)
        {
            var response = await _httpClient.GetAsync($"/api/events/?categoryId={categoryid}");
            return await response.ReadContentAs<List<Event>>();
        }

        public async Task<Event> GetEvent(Guid id)
        {
            var response = await _httpClient.GetAsync($"/api/events/{id}");
            return await response.ReadContentAs<Event>();
        }

        public async Task<IEnumerable<Category>> GetCategories()
        {
            var response = await _httpClient.GetAsync("/api/categories");
            return await response.ReadContentAs<List<Category>>();
        }
    }
}
