using System;
using System.ComponentModel.DataAnnotations;

namespace BookMyEvent.Services.ShoppingCart.DTOs
{
    public class BasketForCreation
    {
        [Required]
        public Guid UserId { get; set; }
    }
}
