using System;

namespace BookMyEvent.Services.Discount.DTOs
{
    public class CouponDTO
    {
        public Guid CouponId { get; set; }
        public Guid UserId { get; set; }
        public int Amount { get; set; }
    }
}
