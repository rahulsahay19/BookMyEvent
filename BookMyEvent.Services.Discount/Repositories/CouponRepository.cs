using BookMyEvent.Services.Discount.DbContexts;
using BookMyEvent.Services.Discount.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace BookMyEvent.Services.Discount.Repositories
{
    public class CouponRepository : ICouponRepository
    {
        private readonly DiscountDbContext _discountDbContext;

        public CouponRepository(DiscountDbContext discountDbContext)
        {
            _discountDbContext = discountDbContext;
        }

        public async Task<Coupon> GetCouponById(Guid couponId)
        {
            return await _discountDbContext.Coupons.Where(x => x.CouponId == couponId).FirstOrDefaultAsync();
        }

        public async Task<Coupon> GetCouponByUserId(Guid userId)
        {
            // TODO!
            return await _discountDbContext.Coupons.Where(x => x.UserId == userId).FirstOrDefaultAsync();
        }
    }
}
