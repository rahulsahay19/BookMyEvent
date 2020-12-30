using BookMyEvent.Integration.Messages;
using System.Threading.Tasks;

namespace BookMyEvent.Integration.MessagingBus
{
    public interface IMessageBus
    {
        Task PublishMessage(IntegrationBaseMessage message, string topicName);
    }
}
