using BookMyEvent.Web.Services;
using BookMyEvent.Web.View;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace BookMyEvent.Web.Controllers
{
    public class EventCatalogController : Controller
    {
        private readonly IEventCatalogService eventCatalogService;
        private readonly Settings settings;

        public EventCatalogController(IEventCatalogService eventCatalogService, Settings settings)
        {
            this.eventCatalogService = eventCatalogService;
            this.settings = settings;
        }

        public async Task<IActionResult> Index(Guid categoryId)
        {
            var getCategories = eventCatalogService.GetCategories();
            var getEvents = categoryId == Guid.Empty ? eventCatalogService.GetAll() :
                eventCatalogService.GetByCategoryId(categoryId);
            await Task.WhenAll(new Task[] { getCategories, getEvents });

            return View(
                new EventListModel
                {
                    Events = getEvents.Result,
                    Categories = getCategories.Result,
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

