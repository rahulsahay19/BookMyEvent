using System;
using System.ComponentModel.DataAnnotations;

namespace BookMyEvent.Web.Models.Api
{
    public class CouponForUpdate
    {
        [Required]
        public Guid CouponId { get; set; }
    }
}
