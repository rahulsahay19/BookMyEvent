using BookMyEvent.Gateway.Shared.Basket;
using BookMyEvent.Gateway.WebBff.DTOs;
using BookMyEvent.Gateway.WebBff.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace BookMyEvent.Gateway.WebBff.Controllers
{
    [ApiController]
    [Route("api/bffweb/events")]
    public class CatalogController : ControllerBase
    {
        private readonly ICatalogService catalogService;
        private readonly IBasketService basketService;

        public CatalogController(ICatalogService catalogService, IBasketService basketService)
        {
            this.catalogService = catalogService;
            this.basketService = basketService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllEvents(Guid categoryId)
        {
            var allevents = await catalogService.GetAllEvents();
            return Ok(allevents);
        }

        [HttpGet("{categoryId}")]
        public async Task<IActionResult> GetEventsPerCategory(Guid categoryId)
        {
            var eventsPerCategory = await catalogService.GetEventsPerCategory(categoryId);
            return Ok(eventsPerCategory);
        }

        [HttpGet("event/{eventId}")]
        public async Task<IActionResult> GetEventById(Guid eventId)
        {
            var eventById = await catalogService.GetEventById(eventId);
            return Ok(eventById);
        }

        [HttpGet("categories")]
        public async Task<IActionResult> GetAllCategories()
        {
            var categories = await catalogService.GetAllCategories();
            return Ok(categories);
        }

        [HttpGet("catalogbrowse/{categoryId}/basket/{basketId}")]
        public async Task<IActionResult> GetCatalogBrowse(Guid basketId, Guid categoryId)
        {

            var getBasket = basketId == Guid.Empty ? Task.FromResult<BasketDto>(null) :
                basketService.GetBasket(basketId);

            var getCategories = catalogService.GetAllCategories();

            var getEvents = categoryId == Guid.Empty ? catalogService.GetAllEvents() :
                catalogService.GetEventsPerCategory(categoryId);

            await Task.WhenAll(new Task[] { getBasket, getCategories, getEvents });

            var numberOfItems = getBasket.Result?.NumberOfItems ?? 0;

            return Ok(new CatalogBrowseDto
            {
                Events = getEvents.Result,
                Categories = getCategories.Result,
                NumberOfItems = numberOfItems,
            });
        }
    }
}
