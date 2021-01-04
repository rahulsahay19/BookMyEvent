using BookMyEvent.Web.Bff.Extensions;
using BookMyEvent.Web.Bff.Models.Api;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace BookMyEvent.Web.Bff.Services
{
    public class OrderService : IOrderService
    {
        private readonly HttpClient client;

        public OrderService(HttpClient client)
        {
            this.client = client;
        }

        public async Task<List<Order>> GetOrdersForUser(Guid userId)
        {
            var response = await client.GetAsync($"/api/order/user/{userId}");
            return await response.ReadContentAs<List<Order>>();
        }

        public async Task<Order> GetOrderDetails(Guid orderId)
        {
            var response = await client.GetAsync($"/api/order/{orderId}");
            return await response.ReadContentAs<Order>();
        }
    }
}
