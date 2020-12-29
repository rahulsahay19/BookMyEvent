using BookMyEvent.Messages;
using Rebus.Handlers;
using System;
using System.Threading.Tasks;

namespace BookMyEvent.Services.Payment
{
    public class NewOrderHandler : IHandleMessages<PaymentRequestMessage>
    {
        public Task Handle(PaymentRequestMessage message)
        {
            Console.WriteLine($"Payment request received for basket id {message.BasketId}.");
            return Task.CompletedTask;
        }
    }
}
