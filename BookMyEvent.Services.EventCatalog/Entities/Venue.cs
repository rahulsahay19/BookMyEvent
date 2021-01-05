using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BookMyEvent.Services.EventCatalog.Entities
{
    public class Venue
    {
        [Required]
        public Guid VenueId { get; set; }
        public string Name { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public List<Event> Events { get; set; }
    }
}
