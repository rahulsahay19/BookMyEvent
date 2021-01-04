using System;

namespace BookMyEvent.Web.Bff.Models.Api
{
    public class PriceUpdate
    {
        public Guid EventId { get; set; }
        public int Price { get; set; }
    }
}
