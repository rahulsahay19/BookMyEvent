namespace BookMyEvent.Services.EventCatalog.Entities
{
    public class IntegrationEventLog
    {
        public int IntegrationEventLogId { get; set; }
        public string IntegrationEventType { get; set; }
        public string ServiceBusTopicName { get; set; }
        public string IntegrationEventBody { get; set; }
        public string State { get; set; }
    }
}
