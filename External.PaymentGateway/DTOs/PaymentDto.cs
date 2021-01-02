using System;

namespace External.PaymentGateway.DTOs
{
    public class PaymentDto
    {
        public Guid OrderId { get; set; }
        public int Total { get; set; }
    }
}
