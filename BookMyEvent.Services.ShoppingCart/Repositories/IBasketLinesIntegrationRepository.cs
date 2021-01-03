using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookMyEvent.Services.ShoppingCart.Repositories
{
    public interface IBasketLinesIntegrationRepository
    {
        Task UpdatePricesForIntegrationEvent(DTOs.PriceUpdate priceUpdate);
    }
}
