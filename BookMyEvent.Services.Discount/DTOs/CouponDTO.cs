using System;

namespace BookMyEvent.Services.Discount.DTOs
{
    public class CouponDTO
    {
        public Guid CouponId { get; set; }
        public string Code { get; set; }
        public int Amount { get; set; }
        public bool AlreadyUsed { get; set; }
    }
}
