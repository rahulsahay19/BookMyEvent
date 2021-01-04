using BookMyEvent.Web.Bff.Extensions;
using BookMyEvent.Web.Bff.Models;
using BookMyEvent.Web.Bff.Models.Api;
using BookMyEvent.Web.Bff.Models.View;
using BookMyEvent.Web.Bff.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace BookMyEvent.Web.Bff.Bff.Controllers
{
    public class EventCatalogController : Controller
    {
        private readonly IEventCatalogService eventCatalogService;
        private readonly IShoppingBasketService shoppingBasketService;
        private readonly Settings settings;

        public EventCatalogController(IEventCatalogService eventCatalogService, IShoppingBasketService shoppingBasketService, Settings settings)
        {
            this.eventCatalogService = eventCatalogService;
            this.shoppingBasketService = shoppingBasketService;
            this.settings = settings;
        }

        //public async Task<IActionResult> Index(Guid categoryId)
        //{
        //    var currentBasketId = Request.Cookies.GetCurrentBasketId(settings);

        //    var getBasket = currentBasketId == Guid.Empty ? Task.FromResult<Basket>(null) :
        //        shoppingBasketService.GetBasket(currentBasketId);

        //    var getCategories = eventCatalogService.GetCategories();

        //    var getEvents = categoryId == Guid.Empty ? eventCatalogService.GetAll() :
        //        eventCatalogService.GetByCategoryId(categoryId);

        //    await Task.WhenAll(new Task[] { getBasket, getCategories, getEvents });

        //    var numberOfItems = getBasket.Result?.NumberOfItems ?? 0;

        //    return View(
        //        new EventListModel
        //        {
        //            Events = getEvents.Result,
        //            Categories = getCategories.Result,
        //            NumberOfItems = numberOfItems,
        //            SelectedCategory = categoryId
        //        }
        //    );
        //}

        public async Task<IActionResult> Index(Guid categoryId)
        {
            var currentBasketId = Request.Cookies.GetCurrentBasketId(settings);
            var result = await eventCatalogService.GetCatalogBrowse(currentBasketId, categoryId);

            return View(
                new EventListModel
                {
                    Events = result.Events,
                    Categories = result.Categories,
                    NumberOfItems = result.NumberOfItems,
                    SelectedCategory = categoryId
                }
            );
        }

        [HttpPost]
        public IActionResult SelectCategory([FromForm] Guid selectedCategory)
        {
            return RedirectToAction("Index", new { categoryId = selectedCategory });
        }

        public async Task<IActionResult> Detail(Guid eventId)
        {
            var ev = await eventCatalogService.GetEvent(eventId);
            return View(ev);
        }
    }
}
