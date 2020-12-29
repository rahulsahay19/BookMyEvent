using Microsoft.Azure.Storage;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Rebus.Activation;
using Rebus.Config;
using System.Threading;
using System.Threading.Tasks;

namespace BookMyEvent.Services.Payment
{
    public class NewOrderWorkerService : BackgroundService
    {
        private readonly IConfiguration _config;

        public NewOrderWorkerService(IConfiguration config)
        {
            _config = config;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            // Storage queue configuration
            var storageAccount = CloudStorageAccount.Parse(
                _config["AzureQueues:ConnectionString"]);

            using var activator = new BuiltinHandlerActivator();
            activator.Register(() => new NewOrderHandler());
            Configure.With(activator)
                .Transport(t => t.UseAzureStorageQueues(
                    storageAccount, _config["AzureQueues:QueueName"]))
                .Start(); // started listening for the queue

            //To prevent this process from exiting, I am delaying this process, indefinetely
            // by specfying cancellation token
            await Task.Delay(Timeout.InfiniteTimeSpan, stoppingToken);
        }
    }
}
