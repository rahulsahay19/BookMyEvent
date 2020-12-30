using BookMyEvent.Services.Marketing.Entities;
using System.Threading.Tasks;

namespace BookMyEvent.Services.Marketing.Repositories
{
    public interface IBasketChangeEventRepository
    {
        Task AddBasketChangeEvent(BasketChangeEvent basketChangeEvent);
    }
}