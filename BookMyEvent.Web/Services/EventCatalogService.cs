using BookMyEvent.Web.Extensions;
using BookMyEvent.Web.Models.Api;
using IdentityModel.Client;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;

namespace BookMyEvent.Web.Services
{
    public class EventCatalogService : IEventCatalogService
    {
        private readonly HttpClient client;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public EventCatalogService(HttpClient client, IHttpContextAccessor httpContextAccessor)
        {
            this.client = client;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<IEnumerable<Event>> GetAll()
        {
            client.SetBearerToken(await _httpContextAccessor.HttpContext.GetTokenAsync("access_token"));
            var response = await client.GetAsync("api/events");
            return await response.ReadContentAs<List<Event>>();
        }

        public async Task<IEnumerable<Event>> GetByCategoryId(Guid categoryid)
        {
            client.SetBearerToken(await _httpContextAccessor.HttpContext.GetTokenAsync("access_token"));
            var response = await client.GetAsync($"api/events/?categoryId={categoryid}");
            return await response.ReadContentAs<List<Event>>();
        }

        public async Task<Event> GetEvent(Guid id)
        {
            client.SetBearerToken(await _httpContextAccessor.HttpContext.GetTokenAsync("access_token"));
            var response = await client.GetAsync($"api/events/{id}");
            return await response.ReadContentAs<Event>();
        }

        public async Task<IEnumerable<Category>> GetCategories()
        {
            client.SetBearerToken(await _httpContextAccessor.HttpContext.GetTokenAsync("access_token"));
            var response = await client.GetAsync("api/categories");
            return await response.ReadContentAs<List<Category>>();
        }

        public async Task<PriceUpdate> UpdatePrice(PriceUpdate priceUpdate)
        {
            client.SetBearerToken(await _httpContextAccessor.HttpContext.GetTokenAsync("access_token"));
            var response = await client.PostAsJson($"api/events/eventpriceupdate", priceUpdate);
            return await response.ReadContentAs<PriceUpdate>();
        }

        public async Task<EventUpdate> UpdateEvent(EventUpdate eventUpdate)
        {
            client.SetBearerToken(await _httpContextAccessor.HttpContext.GetTokenAsync("access_token"));
            var response = await client.PostAsJson($"api/events/eventupdate", eventUpdate);
            return await response.ReadContentAs<EventUpdate>();
        }

    }
}

