using BookMyEvent.Services.IntegrationEventPublisher.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookMyEvent.Services.IntegrationEventPublisher.DbContexts
{
    public class IntegrationEventsDbContext : DbContext
    {
        public IntegrationEventsDbContext(DbContextOptions<IntegrationEventsDbContext> options) : base(options)
        {

        }
        public DbSet<IntegrationEventLogEntry> IntegrationEventLogEntries { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<IntegrationEventLogEntry>(entity => {
                entity.ToTable("IntegrationEventLogs");
            });
        }

    }
}
