using BookMyEvent.Services.IntegrationEventPublisher.DbContexts;
using BookMyEvent.Services.IntegrationEventPublisher.Entities;
using BookMyEvent.Services.IntegrationEventPublisher.Messages;
using BookMyEvent.Services.IntegrationEventPublisher.Repositories;
using BookMyEvent.Integration.Messages;
using BookMyEvent.Integration.MessagingBus;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace BookMyEvent.Services.IntegrationEventPublisher.Worker
{
    public class EventPublisher : BackgroundService
    {
        private readonly ILogger _logger;
        private readonly IConfiguration configuration;
        private readonly IMessageBus messageBus;
        private readonly string eventChangedMessageTopic;
        private readonly IntegrationEventRepository _repository;

        public EventPublisher(IConfiguration configuration, ILoggerFactory loggerFactory, IMessageBus messageBus, IntegrationEventRepository repository)
        {
            _logger = loggerFactory.CreateLogger<EventPublisher>();
            eventChangedMessageTopic = configuration.GetValue<string>("EventChangedMessageTopic");
            this.configuration = configuration;
            this.messageBus = messageBus;
            _repository = repository;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogDebug("EventPublisher is starting.");

            stoppingToken.Register(() => _logger.LogDebug("EventPublisher background task is stopping."));

            while (!stoppingToken.IsCancellationRequested)
            {
                _logger.LogDebug("EventPublisher background task is doing background work.");

                await CheckEventsForPublishing();

                await Task.Delay(configuration.GetValue<int>("CheckUpdateTime"), stoppingToken);
            }

            _logger.LogDebug("EventPublisher background task is stopping.");

            await Task.CompletedTask;
        }

        private async Task CheckEventsForPublishing()
        {
            _logger.LogDebug("Checking for unpublished events");

            var unpublishedEvents = await _repository.GetUnpublishedEvents();

            foreach (var eventToPublish in unpublishedEvents)
            {
                _logger.LogInformation("----- Publishing integration event: {IntegrationEventId} to Service Bus Topic: {ServiceBusTopicName}", eventToPublish.IntegrationEventLogId, eventToPublish.ServiceBusTopicName);
                try
                {
                    var message = JsonConvert.DeserializeObject<EventUpdatedMessage>(eventToPublish.IntegrationEventBody);

                    await _repository.UpdateIntegrationEventLogEntryState(eventToPublish, "In Process");

                    await messageBus.PublishMessage(message, eventToPublish.ServiceBusTopicName);

                    await _repository.UpdateIntegrationEventLogEntryState(eventToPublish, "Published");
                }
                catch (Exception ex)
                {
                    _logger.LogInformation("----- Error publishing integration event: {IntegrationEventId}.  Exception:{ex}", eventToPublish.IntegrationEventLogId, ex.ToString());
                }
            }
        }

    }
}
