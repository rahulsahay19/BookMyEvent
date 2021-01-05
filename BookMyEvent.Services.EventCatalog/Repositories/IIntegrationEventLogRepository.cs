using BookMyEvent.Services.EventCatalog.Entities;

namespace BookMyEvent.Services.EventCatalog.Repositories
{
    public interface IIntegrationEventLogRepository
    {
        void AddEventLogEntry(IntegrationEventLog logEntry);
    }
}
