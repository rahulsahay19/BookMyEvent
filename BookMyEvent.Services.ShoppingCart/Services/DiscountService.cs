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
            //this.client.DefaultRequestHeaders.Add("Accept", "...");
            //this.client.BaseAddress = new Uri("...");
        }

        public async Task<Coupon> GetCoupon(Guid couponId)
        {
            var response = await client.GetAsync($"/api/discount/{couponId}");
            return await response.ReadContentAs<Coupon>();
        }

        public async Task<Coupon> GetCouponWithError(Guid couponId)
        {
            var response = await client.GetAsync($"/api/discount/error/{couponId}");
            return await response.ReadContentAs<Coupon>();
        }
    }
}
