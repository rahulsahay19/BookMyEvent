using BookMyEvent.Web.Models;
using BookMyEvent.Web.Models.Api;
using BookMyEvent.Web.Models.View;
using BookMyEvent.Web.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace BookMyEvent.Web.Controllers
{
    public class AdminController : Controller
    {
        private readonly IEventCatalogService eventCatalogService;

        public AdminController(IEventCatalogService eventCatalogService, Settings settings)
        {
            this.eventCatalogService = eventCatalogService;
        }

        public async Task<IActionResult> Index()
        {
            var allEvents = await eventCatalogService.GetAll();

            return View(allEvents);
        }

        public async Task<IActionResult> Details(Guid eventId)
        {
            var selectedEvent = (await eventCatalogService.GetAll()).Where(x => x.EventId == eventId).FirstOrDefault();

            var vm = new EventUpdateViewModel()
            {
                EventId = selectedEvent.EventId,
                Name = selectedEvent.Name,
                Date = selectedEvent.Date,
                Price = selectedEvent.Price,
                Message = ""
            };

            return View(vm);
        }

        [HttpPost]
        public async Task<IActionResult> Details(EventUpdateViewModel eventUpdateViewModel)
        {
            EventUpdate eventUpdate = new EventUpdate()
            {
                EventId = eventUpdateViewModel.EventId,
                Price = eventUpdateViewModel.Price,
                Name = eventUpdateViewModel.Name,
                Date = eventUpdateViewModel.Date,
                Message = eventUpdateViewModel.Message
            };

            await eventCatalogService.UpdateEvent(eventUpdate);

            return RedirectToAction("Index");
        }
    }

}
