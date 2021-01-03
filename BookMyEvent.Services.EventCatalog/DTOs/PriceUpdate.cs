using System;

namespace BookMyEvent.Services.EventCatalog.DTOs
{
    public class PriceUpdate
    {
        public Guid EventId { get; set; }
        public int Price { get; set; }
    }
}
