using BookMyEvent.Services.ShoppingCart.DTOs;
using BookMyEvent.Services.ShoppingCart.Extensions;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace BookMyEvent.Services.ShoppingCart.Services
{
    public class DiscountService : IDiscountService
    {
        private readonly HttpClient client;

        public DiscountService(HttpClient client)
        {
            this.client = client;
        }

        public async Task<Coupon> GetCoupon(Guid userId)
        {
            var response = await client.GetAsync($"/api/discount/user/{userId}");
            if (!response.IsSuccessStatusCode)
            {
                return null;
            }
            return await response.ReadContentAs<Coupon>();
        }
    }
}
