using BookMyEvent.Web.Bff.Extensions;
using BookMyEvent.Web.Bff.Models.Api;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace BookMyEvent.Web.Bff.Services
{
    public class DiscountService : IDiscountService
    {
        private readonly HttpClient client;

        public DiscountService(HttpClient client)
        {
            this.client = client;
        }

        public async Task<Coupon> GetCouponByCode(string code)
        {
            if (code == string.Empty)
                return null;

            var response = await client.GetAsync($"/api/discount/code/{code}");
            return await response.ReadContentAs<Coupon>();
        }

        public async Task<Coupon> GetCouponById(Guid couponId)
        {
            var response = await client.GetAsync($"/api/discount/{couponId}");
            return await response.ReadContentAs<Coupon>();
        }

        public async Task UseCoupon(Guid couponId)
        {
            var response = await client.PutAsJson($"/api/discount/use/{couponId}", new CouponForUpdate());
        }
    }
}
