using BookMyEvent.Services.Discount.Entities;
using System;
using System.Threading.Tasks;

namespace BookMyEvent.Services.Discount.Repositories
{
    public interface ICouponRepository
    {
        Task<Coupon> GetCouponByUserId(Guid userId);
        Task<Coupon> GetCouponById(Guid couponId);
    }
}
