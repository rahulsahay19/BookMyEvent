using BookMyEvent.Integration.Messages;
using System;

namespace BookMyEvent.Services.EventCatalog.Messages
{
    public class PriceUpdatedMessage : IntegrationBaseMessage
    {
        public Guid EventId { get; set; }
        public int Price { get; set; }
    }
}
