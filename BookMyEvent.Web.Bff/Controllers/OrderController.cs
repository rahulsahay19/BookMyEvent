using BookMyEvent.Web.Bff.Models;
using BookMyEvent.Web.Bff.Models.View;
using BookMyEvent.Web.Bff.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace BookMyEvent.Web.Bff.Bff.Controllers
{
    public class OrderController : Controller
    {
        private readonly IOrderService orderService;
        private readonly Settings settings;

        public OrderController(Settings settings, IOrderService orderService)
        {
            this.settings = settings;
            this.orderService = orderService;
        }

        public async Task<IActionResult> Index()
        {
            var orders = await orderService.GetOrdersForUser(settings.UserId);

            return View(new OrderViewModel { Orders = orders });
        }
    }
}
