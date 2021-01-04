using BookMyEvent.Gateway.Shared.Event;
using System.Collections.Generic;

namespace BookMyEvent.Gateway.WebBff.DTOs
{
    public class CatalogBrowseDto
    {
        public IEnumerable<EventDto> Events { get; set; }
        public IEnumerable<CategoryDto> Categories { get; set; }
        public int NumberOfItems { get; set; }
    }
}
