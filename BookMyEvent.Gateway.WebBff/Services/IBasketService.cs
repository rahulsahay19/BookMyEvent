using BookMyEvent.Gateway.Shared.Basket;
using System;
using System.Threading.Tasks;

namespace BookMyEvent.Gateway.WebBff.Services
{
    public interface IBasketService
    {
        Task<BasketDto> GetBasket(Guid basketId);
    }
}