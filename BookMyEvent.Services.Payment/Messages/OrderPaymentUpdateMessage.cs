
using BookMyEvent.Integration.Messages;
using System;

namespace BookMyEvent.Services.Payment.Messages
{
    public class OrderPaymentUpdateMessage : IntegrationBaseMessage
    {
        public Guid OrderId { get; set; }
        public bool PaymentSuccess { get; set; }
    }
}
