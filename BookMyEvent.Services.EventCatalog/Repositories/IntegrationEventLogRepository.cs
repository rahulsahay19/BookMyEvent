using BookMyEvent.Services.EventCatalog.DbContexts;
using BookMyEvent.Services.EventCatalog.Entities;

namespace BookMyEvent.Services.EventCatalog.Repositories
{
    public class IntegrationEventLogRepository : IIntegrationEventLogRepository
    {
        private readonly EventCatalogDbContext _dbContext;

        public IntegrationEventLogRepository(EventCatalogDbContext dbContext)
        {
            this._dbContext = dbContext;
        }

        public void AddEventLogEntry(IntegrationEventLog logEntry)
        {
            _dbContext.IntegrationEventLogs.Add(logEntry);
        }
    }
}
