using AutoMapper;

namespace BookMyEvent.Services.ShoppingCart.Profiles
{
    public class BasketProfile : Profile
    {
        public BasketProfile()
        {
            CreateMap<DTOs.BasketForCreation, Entities.Basket>();
            CreateMap<Entities.Basket, DTOs.Basket>().ReverseMap();
        }
    }
}
