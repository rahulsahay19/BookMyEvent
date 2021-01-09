using AutoMapper;
using BookMyEvent.Integration.MessagingBus;
using BookMyEvent.Services.ShoppingCart.DTOs;
using BookMyEvent.Services.ShoppingCart.Messages;
using BookMyEvent.Services.ShoppingCart.Repositories;
using BookMyEvent.Services.ShoppingCart.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Polly.CircuitBreaker;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace BookMyEvent.Services.ShoppingCart.Controllers
{
    [Route("api/baskets")]
    [ApiController]
    public class BasketsController : ControllerBase
    {
        private readonly IBasketRepository basketRepository;
        private readonly IMapper mapper;
        private readonly IMessageBus messageBus;
        private readonly IDiscountService discountService;

        public BasketsController(IBasketRepository basketRepository,
            IMapper mapper,
            IMessageBus messageBus,
            IDiscountService discountService)
        {
            this.basketRepository = basketRepository;
            this.mapper = mapper;
            this.messageBus = messageBus;
            this.discountService = discountService;
        }

        [HttpGet("{basketId}", Name = "GetBasket")]
        public async Task<ActionResult<Basket>> Get(Guid basketId)
        {
            var basket = await basketRepository.GetBasketById(basketId);
            if (basket == null)
            {
                return NotFound();
            }

            var result = mapper.Map<Basket>(basket);
            result.NumberOfItems = basket.BasketLines.Sum(bl => bl.TicketAmount);
            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult<Basket>> Post(BasketForCreation basketForCreation)
        {
            var basketEntity = mapper.Map<Entities.Basket>(basketForCreation);

            basketRepository.AddBasket(basketEntity);
            await basketRepository.SaveChanges();

            var basketToReturn = mapper.Map<Basket>(basketEntity);

            return CreatedAtRoute(
                "GetBasket",
                new { basketId = basketEntity.BasketId },
                basketToReturn);
        }

        [HttpPost("checkout")]
        [ProducesResponseType((int)HttpStatusCode.Accepted)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> CheckoutBasketAsync([FromBody] BasketCheckout basketCheckout)
        {
            try
            {
                //based on basket checkout, fetch the basket lines from repo
                var basket = await basketRepository.GetBasketById(basketCheckout.BasketId);

                if (basket == null)
                {
                    return BadRequest();
                }

                var basketCheckoutMessage = mapper.Map<BasketCheckoutMessage>(basketCheckout);
                basketCheckoutMessage.BasketLines = new List<BasketLineMessage>();
                int total = 0;

                foreach (var b in basket.BasketLines)
                {
                    var basketLineMessage = new BasketLineMessage
                    {
                        BasketLineId = b.BasketLineId,
                        Price = b.Price,
                        TicketAmount = b.TicketAmount
                    };

                    total += b.Price * b.TicketAmount;

                    basketCheckoutMessage.BasketLines.Add(basketLineMessage);
                }

                //apply discount by talking to the discount service
                Coupon coupon = null;

                // This is unverified information, hence we should not use it from view model
                //var userId = basketCheckout.UserId;
                var userId = Guid.Parse(User.Claims.FirstOrDefault(c=>c.Type=="sub")?.Value ?? string.Empty);
                //TODO not working Rather read the same from gateway, which is part of the claims
                //var userId = Guid.Parse(HttpContext.Request.Headers["CurrentUser"][0]);

                if (!(userId == Guid.Empty))
                    coupon = await discountService.GetCoupon(userId);

                if (coupon != null)
                {
                    basketCheckoutMessage.BasketTotal = total - coupon.Amount;
                }
                else
                {
                    basketCheckoutMessage.BasketTotal = total;
                }

                try
                {
                    await messageBus.PublishMessage(basketCheckoutMessage, "checkoutmessage");
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    throw;
                }

                await basketRepository.ClearBasket(basketCheckout.BasketId);
                return Accepted(basketCheckoutMessage);
            }
            catch (BrokenCircuitException ex)
            {
                string message = ex.Message;
                return StatusCode(StatusCodes.Status500InternalServerError, ex.StackTrace);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.StackTrace);
            }
        }
    }
}
