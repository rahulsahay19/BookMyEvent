using BookMyEvent.Integration.Messages;
using System;

namespace BookMyEvent.Services.Ordering.Messages
{
    public class OrderPaymentRequestMessage : IntegrationBaseMessage
    {
        public Guid OrderId { get; set; }
        public int Total { get; set; }
        public string CardNumber { get; set; }
        public string CardName { get; set; }
        public string CardExpiration { get; set; }
    }
}
