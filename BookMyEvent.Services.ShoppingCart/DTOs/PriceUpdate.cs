using System;

namespace BookMyEvent.Services.ShoppingCart.DTOs
{
    public class PriceUpdate
    {
        public Guid EventId { get; set; }
        public int Price { get; set; }
    }
}
