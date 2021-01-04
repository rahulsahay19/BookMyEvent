using BookMyEvent.Web.Bff.Models.Api;
using System;
using System.Threading.Tasks;

namespace BookMyEvent.Web.Bff.Services
{
    public interface IDiscountService
    {
        Task<Coupon> GetCouponByCode(string code);
        Task UseCoupon(Guid couponId);
        Task<Coupon> GetCouponById(Guid couponId);
    }
}
