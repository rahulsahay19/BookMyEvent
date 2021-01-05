using BookMyEvent.Services.EventCatalog.DbContexts;

namespace BookMyEvent.Services.EventCatalog.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly EventCatalogDbContext _dbContext;
        private IIntegrationEventLogRepository _integrationEventLogRepository;
        private IEventRepository _eventRepository;

        public UnitOfWork(EventCatalogDbContext databaseContext)
        { _dbContext = databaseContext; }

        public IEventRepository EventRepository
        {
            get { return _eventRepository = _eventRepository ?? new EventRepository(_dbContext); }
        }

        public IIntegrationEventLogRepository IntegrationEventLogRepository
        {
            get { return _integrationEventLogRepository = _integrationEventLogRepository ?? new IntegrationEventLogRepository(_dbContext); }
        }

        public void Commit()
        { _dbContext.SaveChanges(); }

        public void Rollback()
        { _dbContext.Dispose(); }
    }
}
