using BookMyEvent.Services.ShoppingCart.Entities;
using BookMyEvent.Services.ShoppingCart.Extensions;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace BookMyEvent.Services.ShoppingCart.Services
{
    public class EventCatalogService : IEventCatalogService
    {
        private readonly HttpClient client;

        public EventCatalogService(HttpClient client)
        {
            this.client = client;
        }

        public async Task<Event> GetEvent(Guid id)
        {
            var response = await client.GetAsync($"/api/events/{id}");
            return await response.ReadContentAs<Event>();
        }
    }
}