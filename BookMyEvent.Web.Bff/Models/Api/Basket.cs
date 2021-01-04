using System;

namespace BookMyEvent.Web.Bff.Models.Api
{
    public class Basket
    {
        public Guid BasketId { get; set; }
        public Guid UserId { get; set; }
        public int NumberOfItems { get; set; }
        public string Code { get; set; }
        public int Discount { get; set; }
        public Guid? CouponId { get; set; }
    }
}
