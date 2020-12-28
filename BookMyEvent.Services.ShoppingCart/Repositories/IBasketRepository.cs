using BookMyEvent.Services.ShoppingCart.Entities;
using System;
using System.Threading.Tasks;

namespace BookMyEvent.Services.ShoppingCart.Repositories
{
    public interface IBasketRepository
    {
        Task<bool> BasketExists(Guid basketId);

        Task<Basket> GetBasketById(Guid basketId);

        void AddBasket(Basket basket);

        Task<bool> SaveChanges();
    }
}
