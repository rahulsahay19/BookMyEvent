using AutoMapper;
using BookMyEvent.Services.Ordering.DTOs;
using BookMyEvent.Services.Ordering.Repositories;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BookMyEvent.Services.Ordering.Controllers
{
    [ApiController]
    [Route("api/order")]
    public class OrderController : Controller
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IMapper mapper;

        public OrderController(IOrderRepository orderRepository, IMapper mapper)
        {
            _orderRepository = orderRepository;
            this.mapper = mapper;
        }

        [HttpGet("user/{userId}")]
        public async Task<IActionResult> List(Guid userId)
        {
            var orders = await _orderRepository.GetOrdersForUser(userId);
            var ordersToReturn = mapper.Map<IEnumerable<OrderDto>>(orders);
            return Ok(ordersToReturn);
        }

        [HttpGet("{orderId}")]
        public async Task<IActionResult> Get(Guid orderId)
        {
            var order = await _orderRepository.GetOrderById(orderId);
            var orderToReturn = mapper.Map<OrderDto>(order);
            return Ok(orderToReturn);
        }

    }
}
