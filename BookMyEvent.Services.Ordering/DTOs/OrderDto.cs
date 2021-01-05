using BookMyEvent.Services.Ordering.DTOs;
using System;
using System.Collections.Generic;

namespace BookMyEvent.Services.Ordering.DTOs
{
    public class OrderDto
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public int OrderTotal { get; set; }
        public DateTime OrderPlaced { get; set; }
        public bool OrderPaid { get; set; }
        public List<OrderLineDto> OrderLines { get; set; }
        public string Message { get; set; }
    }
}
