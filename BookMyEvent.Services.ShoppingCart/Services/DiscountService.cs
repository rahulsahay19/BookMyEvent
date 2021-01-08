using BookMyEvent.Services.ShoppingCart.DTOs;
using BookMyEvent.Services.ShoppingCart.Extensions;
using IdentityModel.Client;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace BookMyEvent.Services.ShoppingCart.Services
{
    public class DiscountService : IDiscountService
    {
        private readonly HttpClient client;
        private readonly IHttpContextAccessor httpContextAccessor;
        private string _accessToken;

        public DiscountService(HttpClient client, IHttpContextAccessor httpContextAccessor)
        {
            this.client = client;
            this.httpContextAccessor = httpContextAccessor;
        }

        public async Task<Coupon> GetCoupon(Guid userId)
        {
            client.SetBearerToken(await GetToken());
            var response = await client.GetAsync($"/api/discount/user/{userId}");
            if (!response.IsSuccessStatusCode)
            {
                return null;
            }
            return await response.ReadContentAs<Coupon>();
        }

        private async Task<string> GetToken()
        {
            if (!string.IsNullOrWhiteSpace(_accessToken))
            {
                return _accessToken;
            }

            // Read the discovery token
            var discoveryDocumentResponse = await client
               .GetDiscoveryDocumentAsync("https://localhost:5012/");
            if (discoveryDocumentResponse.IsError)
            {
                throw new Exception(discoveryDocumentResponse.Error);
            }

            // Creating Custom token request
            //subject_token is the access_token passed in to shopping basket api, which means token which gives access to shopping basket.
            var customParams = new Dictionary<string, string>
            {
                { "subject_token_type", "urn:ietf:params:oauth:token-type:access_token"},
                { "subject_token", await httpContextAccessor.HttpContext.GetTokenAsync("access_token")},
                { "scope", "openid profile discount.fullaccess" }
            };

            var tokenResponse = await client.RequestTokenAsync(new TokenRequest()
            {
                Address = discoveryDocumentResponse.TokenEndpoint,
                GrantType = "urn:ietf:params:oauth:grant-type:token-exchange",
                Parameters = customParams,
                ClientId = "shoppingbaskettodownstreamtokenexchangeclient",
                ClientSecret = "0cdea0bc-779e-4368-b46b-09956f70712c"
            });

            if (tokenResponse.IsError)
            {
                throw new Exception(tokenResponse.Error);
            }

            _accessToken = tokenResponse.AccessToken;
            return _accessToken;
        }
    }
}
