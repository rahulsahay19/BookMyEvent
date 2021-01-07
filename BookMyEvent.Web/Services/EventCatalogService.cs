using BookMyEvent.Web.Extensions;
using BookMyEvent.Web.Models.Api;
using IdentityModel.Client;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace BookMyEvent.Web.Services
{
    public class EventCatalogService : IEventCatalogService
    {
        private readonly HttpClient client;
        private string _accessToken;

        public EventCatalogService(HttpClient client)
        {
            this.client = client;
        }

        private async Task<string> GetToken()
        {
            if (!string.IsNullOrEmpty(_accessToken))
            {
                return _accessToken;
            }

            var discoveryDocumentResponse = await client.GetDiscoveryDocumentAsync("https://localhost:5012");
            if (discoveryDocumentResponse.IsError)
            {
                throw new Exception(discoveryDocumentResponse.Error);
            }

            var tokenResponse = await client.RequestClientCredentialsTokenAsync(
                new ClientCredentialsTokenRequest
                {
                    Address = discoveryDocumentResponse.TokenEndpoint,
                    ClientId = "bookmyeventm2m",
                    ClientSecret = "c89e03c5-7b84-456d-822c-edaca84ed7f4",
                    Scope = "bookmyevent.fullaccess"
                });

            if (tokenResponse.IsError)
            {
                throw new Exception(tokenResponse.Error);
            }

            _accessToken = tokenResponse.AccessToken;
            return _accessToken;
        }

        public async Task<IEnumerable<Event>> GetAll()
        {
            client.SetBearerToken(await GetToken());
            var response = await client.GetAsync("/api/events");
            return await response.ReadContentAs<List<Event>>();
        }

        public async Task<IEnumerable<Event>> GetByCategoryId(Guid categoryid)
        {
            client.SetBearerToken(await GetToken());
            var response = await client.GetAsync($"/api/events/?categoryId={categoryid}");
            return await response.ReadContentAs<List<Event>>();
        }

        public async Task<Event> GetEvent(Guid id)
        {
            client.SetBearerToken(await GetToken());
            var response = await client.GetAsync($"/api/events/{id}");
            return await response.ReadContentAs<Event>();
        }

        public async Task<IEnumerable<Category>> GetCategories()
        {
            client.SetBearerToken(await GetToken());
            var response = await client.GetAsync("/api/categories");
            return await response.ReadContentAs<List<Category>>();
        }

        public async Task<PriceUpdate> UpdatePrice(PriceUpdate priceUpdate)
        {
            client.SetBearerToken(await GetToken());
            var response = await client.PostAsJson($"api/events/eventpriceupdate", priceUpdate);
            return await response.ReadContentAs<PriceUpdate>();
        }

        public async Task<EventUpdate> UpdateEvent(EventUpdate eventUpdate)
        {
            client.SetBearerToken(await GetToken());
            var response = await client.PostAsJson($"api/events/eventupdate", eventUpdate);
            return await response.ReadContentAs<EventUpdate>();
        }

    }
}

