namespace BookMyEvent.Services.EventCatalog.Repositories
{
    public interface IUnitOfWork
    {
        IIntegrationEventLogRepository IntegrationEventLogRepository { get; }
        IEventRepository EventRepository { get; }
        void Commit();
        void Rollback();
    }
}
