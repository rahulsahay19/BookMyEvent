using System;
using System.ComponentModel.DataAnnotations;

namespace BookMyEvent.Web.Bff.Models.Api
{
    public class CouponForUpdate
    {
        [Required]
        public Guid CouponId { get; set; }
    }
}
