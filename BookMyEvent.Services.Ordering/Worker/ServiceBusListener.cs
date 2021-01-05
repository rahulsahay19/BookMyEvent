using BookMyEvent.Services.Ordering.DTOs;
using BookMyEvent.Services.Ordering.Repositories;
using Microsoft.Azure.ServiceBus;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using System;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BookMyEvent.Services.Ordering.Worker
{
    public class ServiceBusListener : IHostedService
    {
        private readonly IConfiguration configuration;
        private ISubscriptionClient subscriptionClient;
        private readonly OrderRepository orderRepository;

        public ServiceBusListener(IConfiguration configuration, OrderRepository orderRepository)
        {
            this.configuration = configuration;
            this.orderRepository = orderRepository;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            subscriptionClient = new SubscriptionClient(configuration.GetValue<string>("ServiceBusConnectionString"), configuration.GetValue<string>("EventUpdatedMessageTopic"), configuration.GetValue<string>("subscriptionName"));

            var messageHandlerOptions = new MessageHandlerOptions(e =>
            {
                ProcessError(e.Exception);
                return Task.CompletedTask;
            })
            {
                MaxConcurrentCalls = 3,
                AutoComplete = false
            };

            subscriptionClient.RegisterMessageHandler(ProcessMessageAsync, messageHandlerOptions);

            return Task.CompletedTask;
        }

        private async Task ProcessMessageAsync(Message message, CancellationToken token)
        {
            var messageBody = Encoding.UTF8.GetString(message.Body);
            EventUpdate eventUpdate = JsonConvert.DeserializeObject<EventUpdate>(messageBody);

            await orderRepository.UpdateOrderEventInformation(eventUpdate);

            await subscriptionClient.CompleteAsync(message.SystemProperties.LockToken);

        }

        public async Task StopAsync(CancellationToken cancellationToken)
        {
            await this.subscriptionClient.CloseAsync();
        }

        protected void ProcessError(Exception e)
        {
        }
    }
}
