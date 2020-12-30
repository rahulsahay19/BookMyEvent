using BookMyEvent.Web.Models.Api;
using System.Collections.Generic;

namespace BookMyEvent.Web.Models.View
{
    public class OrderViewModel
    {
        public IEnumerable<Order> Orders { get; set; }
    }
}