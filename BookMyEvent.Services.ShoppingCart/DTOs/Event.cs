using System;

namespace BookMyEvent.Services.ShoppingCart.DTOs
{
    public class Event
    {
        public Guid EventId { get; set; }
        public string Name { get; set; }
        public DateTime Date { get; set; }
    }
}
