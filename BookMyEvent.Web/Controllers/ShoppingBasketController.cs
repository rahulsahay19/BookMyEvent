using BookMyEvent.Messages;
using BookMyEvent.Web.Extensions;
using BookMyEvent.Web.Models;
using BookMyEvent.Web.Models.Api;
using BookMyEvent.Web.Models.View;
using BookMyEvent.Web.Services;
using Microsoft.AspNetCore.Mvc;
using Rebus.Bus;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace BookMyEvent.Web.Controllers
{
    public class ShoppingBasketController : Controller
    {
        private readonly IShoppingBasketService basketService;
        private readonly IBus bus;
        private readonly Settings settings;

        public ShoppingBasketController(IShoppingBasketService basketService, Settings settings)
        {
            this.basketService = basketService;
            this.settings = settings;
        }

        public async Task<IActionResult> Index()
        {
            var basketLines = await basketService.GetLinesForBasket(Request.Cookies.GetCurrentBasketId(settings));
            var lineViewModels = basketLines.Select(bl => new BasketLineViewModel
            {
                LineId = bl.BasketLineId,
                EventId = bl.EventId,
                EventName = bl.Event.Name,
                Date = bl.Event.Date,
                Price = bl.Price,
                Quantity = bl.TicketAmount
            }
            );
            return View(lineViewModels);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddLine(BasketLineForCreation basketLine)
        {
            var basketId = Request.Cookies.GetCurrentBasketId(settings);
            var newLine = await basketService.AddToBasket(basketId, basketLine);
            Response.Cookies.Append(settings.BasketIdCookieName, newLine.BasketId.ToString());

            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateLine(BasketLineForUpdate basketLineUpdate)
        {
            var basketId = Request.Cookies.GetCurrentBasketId(settings);
            await basketService.UpdateLine(basketId, basketLineUpdate);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> RemoveLine(Guid lineId)
        {
            var basketId = Request.Cookies.GetCurrentBasketId(settings);
            await basketService.RemoveLine(basketId, lineId);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Pay()
        {
            var basketId = Request.Cookies.GetCurrentBasketId(settings);
            await bus.Send(new PaymentRequestMessage { BasketId = basketId });
            return View("Thanks");
        }
    }
}

