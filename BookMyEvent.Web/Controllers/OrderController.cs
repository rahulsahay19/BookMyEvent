using BookMyEvent.Web.Models;
using BookMyEvent.Web.Models.View;
using BookMyEvent.Web.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace BookMyEvent.Web.Controllers
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
