using BookMyEvent.Web.Extensions;
using BookMyEvent.Web.Models;
using BookMyEvent.Web.Models.Api;
using BookMyEvent.Web.Models.View;
using BookMyEvent.Web.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Rebus.Bus;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace BookMyEvent.Web.Controllers
{
    public class ShoppingBasketController : Controller
    {
        private readonly IShoppingBasketService basketService;
        private readonly IDiscountService discountService;
        private readonly Settings settings;
        private readonly IConfiguration config;

        public ShoppingBasketController(IShoppingBasketService basketService, Settings settings, IDiscountService discountService, IConfiguration configuration)
        {
            this.basketService = basketService;
            this.settings = settings;
            this.discountService = discountService;
            this.config = configuration;
        }

        public async Task<IActionResult> Index()
        {
            var basketViewModel = await CreateBasketViewModel();

            return View(basketViewModel);
        }

        private async Task<BasketViewModel> CreateBasketViewModel()
        {
            var basketId = Request.Cookies.GetCurrentBasketId(settings);
            Basket basket = await basketService.GetBasket(basketId);

            var basketLines = await basketService.GetLinesForBasket(basketId);

            var lineViewModels = basketLines.Select(bl => new BasketLineViewModel
            {
                LineId = bl.BasketLineId,
                EventId = bl.EventId,
                EventName = bl.Event.Name,
                Date = bl.Event.Date,
                Price = bl.Price,
                Quantity = bl.TicketAmount
            });


            var basketViewModel = new BasketViewModel
            {
                BasketLines = lineViewModels.ToList()
            };

            Coupon coupon = null;

            if (basket.CouponId.HasValue)
                coupon = await discountService.GetCouponById(basket.CouponId.Value);

            if (coupon != null)
            {
                basketViewModel.Code = coupon.Code;
                basketViewModel.Discount = coupon.Amount;
            }

            basketViewModel.ShoppingCartTotal = basketViewModel.BasketLines.Sum(bl => bl.Price * bl.Quantity);

            return basketViewModel;
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

        public IActionResult Checkout()
        {
            if (config["DefaultUserData:UseDefaultData"] == "True")
            {
                BasketCheckoutViewModel vm = new BasketCheckoutViewModel
                {
                    FirstName = config["DefaultUserData:Data:FirstName"],
                    LastName = config["DefaultUserData:Data:LastName"],
                    Email = config["DefaultUserData:Data:Email"],
                    Address = config["DefaultUserData:Data:Address"],
                    City = config["DefaultUserData:Data:City"],
                    Country = config["DefaultUserData:Data:Country"],
                    ZipCode = config["DefaultUserData:Data:ZipCode"],
                    CardNumber = config["DefaultUserData:Data:CardNumber"],
                    CardName = config["DefaultUserData:Data:CardName"],
                    CardExpiration = config["DefaultUserData:Data:CardExpiration"],
                    CvvCode = config["DefaultUserData:Data:CvvCode"],
                };
                return View(vm);
            }
            else
            {
                return View();
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Checkout(BasketCheckoutViewModel basketCheckoutViewModel)
        {
            try
            {
                var basketId = Request.Cookies.GetCurrentBasketId(settings);
                if (ModelState.IsValid)
                {
                    var basketForCheckout = new BasketForCheckout
                    {
                        FirstName = basketCheckoutViewModel.FirstName,
                        LastName = basketCheckoutViewModel.LastName,
                        Email = basketCheckoutViewModel.Email,
                        Address = basketCheckoutViewModel.Address,
                        ZipCode = basketCheckoutViewModel.ZipCode,
                        City = basketCheckoutViewModel.City,
                        Country = basketCheckoutViewModel.Country,
                        CardNumber = basketCheckoutViewModel.CardNumber,
                        CardName = basketCheckoutViewModel.CardName,
                        CardExpiration = basketCheckoutViewModel.CardExpiration,
                        CvvCode = basketCheckoutViewModel.CvvCode,
                        BasketId = basketId,
                        UserId = Guid.Parse(User.Claims.FirstOrDefault(c => c.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier")?.Value)
                    };

                    await basketService.Checkout(basketCheckoutViewModel.BasketId, basketForCheckout);

                    return RedirectToAction("CheckoutComplete");
                }

                return View(basketCheckoutViewModel);
            }
            catch (Exception e)
            {
                ViewBag.ErrorMessage = e.Message;
                return View(basketCheckoutViewModel);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ApplyDiscountCode(string code)
        {
            var coupon = await discountService.GetCouponByCode(code);

            if (coupon == null || coupon.AlreadyUsed) return RedirectToAction("Index");

            //coupon will be applied to the basket
            var basketId = Request.Cookies.GetCurrentBasketId(settings);
            await basketService.ApplyCouponToBasket(basketId, new CouponForUpdate() { CouponId = coupon.CouponId });
            await discountService.UseCoupon(coupon.CouponId);

            return RedirectToAction("Index");

        }

        public IActionResult CheckoutComplete()
        {
            return View();
        }
    }
}
