using AutoMapper;

namespace BookMyEvent.Services.ShoppingCart.Profiles
{
    public class BasketLineProfile : Profile
    {
        public BasketLineProfile()
        {
            CreateMap<DTOs.BasketLineForCreation, Entities.BasketLine>();
            CreateMap<DTOs.BasketLineForUpdate, Entities.BasketLine>();
            CreateMap<Entities.BasketLine, DTOs.BasketLine>().ReverseMap();
        }
    }
}
