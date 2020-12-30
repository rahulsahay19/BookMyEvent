using BookMyEvent.Services.Marketing.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BookMyEvent.Services.Marketing.Services
{
    public interface IBasketChangeEventService
    {
        Task<List<BasketChangeEvent>> GetBasketChangeEvents(DateTime startDate, int max);
    }
}
