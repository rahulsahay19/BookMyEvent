using BookMyEvent.Services.IntegrationEventPublisher.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookMyEvent.Services.IntegrationEventPublisher.Repositories
{
    public interface IIntegrationEventRepository
    {
        Task<IEnumerable<IntegrationEventLogEntry>> GetUnpublishedEvents();
        Task UpdateIntegrationEventLogEntryState(IntegrationEventLogEntry entry, string state);
    }
}
