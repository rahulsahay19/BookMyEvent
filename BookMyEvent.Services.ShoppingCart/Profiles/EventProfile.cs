using AutoMapper;

namespace BookMyEvent.Services.ShoppingCart.Profiles
{
    public class EventProfile : Profile
    {
        public EventProfile()
        {
            CreateMap<Entities.Event, DTOs.Event>().ReverseMap();
        }
    }
}
