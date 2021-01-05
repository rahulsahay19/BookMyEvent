using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BookMyEvent.Services.IntegrationEventPublisher.Entities
{
    public class IntegrationEventLogEntry
    {
        [Key]
        public int IntegrationEventLogId { get; set; }
        public string IntegrationEventType { get; set; }
        public string ServiceBusTopicName { get; set; }
        public string IntegrationEventBody { get; set; }
        public string State { get; set; }
    }
}
