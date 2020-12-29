using System;

namespace BookMyEvent.Messages
{
    public class PaymentRequestMessage
    {
        //TODO: Extend with basket total amount, CC details, Payment types etc
        public Guid BasketId { get; set; }
    }
}
