using BookMyEvent.Integration.Messages;
using System;

namespace BookMyEvent.Services.EventCatalog.Messages
{
    public class EventUpdatedMessage : IntegrationBaseMessage
    {
        public Guid EventId { get; set; }
        public string Name { get; set; }
        public DateTime Date { get; set; }
        public int Price { get; set; }
        public string Message { get; set; }
    }
}
