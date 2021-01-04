using BookMyEvent.Web.Bff.Models.Api;
using System.Collections.Generic;

namespace BookMyEvent.Web.Bff.Models.View
{
    public class OrderViewModel
    {
        public IEnumerable<Order> Orders { get; set; }
    }
}