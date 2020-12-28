using BookMyEvent.Web.Models.Api;
using System;
using System.Collections.Generic;

namespace BookMyEvent.Web.View
{
    public class EventListModel
    {
        public IEnumerable<Event> Events { get; set; }
        public Guid SelectedCategory { get; set; }
        public IEnumerable<Category> Categories { get; set; }
    }
}
