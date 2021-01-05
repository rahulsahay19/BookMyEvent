using BookMyEvent.Services.Ordering.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BookMyEvent.Services.Ordering.Repositories
{
    public interface IOrderRepository
    {
        Task<List<Order>> GetOrdersForUser(Guid userId);
        Task AddOrder(Order order);
        Task<Order> GetOrderById(Guid orderId);
        Task UpdateOrderEventInformation(DTOs.EventUpdate eventUpdate);
        Task UpdateOrderPaymentStatus(Guid orderId, bool paid);


    }
}