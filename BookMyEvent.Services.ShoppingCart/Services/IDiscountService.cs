using BookMyEvent.Services.ShoppingCart.DTOs;
using System;
using System.Threading.Tasks;

namespace BookMyEvent.Services.ShoppingCart.Services
{
    public interface IDiscountService
    {
        Task<Coupon> GetCoupon(Guid couponId);
        Task<Coupon> GetCouponWithError(Guid couponId);
    }
}
