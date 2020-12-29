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

        public async Task<Coupon> GetCouponByCode(string couponCode)
        {
            return await _discountDbContext.Coupons.Where(x => x.Code == couponCode).FirstOrDefaultAsync();
        }

        public async Task UseCoupon(Guid couponId)
        {
            var couponToUpdate =
                await _discountDbContext.Coupons.Where(x => x.CouponId == couponId).FirstOrDefaultAsync();

            if (couponToUpdate == null)
                throw new Exception();

            couponToUpdate.AlreadyUsed = true;
            await _discountDbContext.SaveChangesAsync();
        }

        public async Task<Coupon> GetCouponById(Guid couponId)
        {
            return await _discountDbContext.Coupons.Where(x => x.CouponId == couponId).FirstOrDefaultAsync();
        }
    }
}
