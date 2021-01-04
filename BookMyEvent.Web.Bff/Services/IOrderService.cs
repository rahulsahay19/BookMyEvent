using BookMyEvent.Web.Bff.Models.Api;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BookMyEvent.Web.Bff.Services
{
    public interface IOrderService
    {
        Task<List<Order>> GetOrdersForUser(Guid userId);
        Task<Order> GetOrderDetails(Guid orderId);
    }
}
