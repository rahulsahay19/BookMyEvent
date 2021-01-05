using System;

namespace BookMyEvent.Web.Models.Api
{
    public class EventUpdate
    {
        public Guid EventId { get; set; }
        public string Name { get; set; }
        public DateTime Date { get; set; }
        public int Price { get; set; }
        public string Message { get; set; }
    }
}
