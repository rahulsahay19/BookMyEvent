
using BookMyEvent.Web.Bff.Models.Api;
using System;
using System.Collections.Generic;

namespace BookMyEvent.Web.Bff.Models.View
{
    public class EventListModel
    {
        public IEnumerable<Event> Events { get; set; }
        public Guid SelectedCategory { get; set; }
        public IEnumerable<Category> Categories { get; set; }
        public int NumberOfItems { get; set; }
    }
}
