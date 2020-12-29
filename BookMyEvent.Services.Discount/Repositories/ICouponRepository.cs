using BookMyEvent.Services.Discount.Entities;
using System;
using System.Threading.Tasks;

namespace BookMyEvent.Services.Discount.Repositories
{
    public interface ICouponRepository
    {
        Task<Coupon> GetCouponByCode(string couponCode);
        Task UseCoupon(Guid couponId);
        Task<Coupon> GetCouponById(Guid couponId);
    }
}
