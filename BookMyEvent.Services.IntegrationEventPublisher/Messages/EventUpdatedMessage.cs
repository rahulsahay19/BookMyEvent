using BookMyEvent.Integration.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookMyEvent.Services.IntegrationEventPublisher.Messages
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
