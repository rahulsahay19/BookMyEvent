using AutoMapper;
using BookMyEvent.Services.Discount.DTOs;
using BookMyEvent.Services.Discount.Repositories;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Net;
using System.Threading.Tasks;

namespace BookMyEvent.Services.Discount.Controllers
{
    [ApiController]
    [Route("api/discount")]
    public class DiscountController : Controller
    {
        private readonly ICouponRepository _couponRepository;
        private readonly IMapper _mapper;

        public DiscountController(ICouponRepository couponRepository, IMapper mapper)
        {
            _couponRepository = couponRepository;
            _mapper = mapper;
        }

        // TODO: get discount for user!! 

        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [HttpGet("user/{userId}")]
        public async Task<IActionResult> GetDiscountForUser(Guid userId)
        {
            var coupon = await _couponRepository.GetCouponByUserId(userId);

            if (coupon == null)
                return NotFound();

            return Ok(_mapper.Map<CouponDTO>(coupon));
        }

        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [HttpGet("{couponId}")]
        public async Task<IActionResult> GetDiscountForCode(Guid couponId)
        {
            var coupon = await _couponRepository.GetCouponById(couponId);

            if (coupon == null)
                return NotFound();

            return Ok(_mapper.Map<CouponDTO>(coupon));
        }
    }
}
    