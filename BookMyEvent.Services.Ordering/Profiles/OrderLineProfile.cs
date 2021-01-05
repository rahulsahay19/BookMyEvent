using AutoMapper;

namespace BookMyEvent.Services.Ordering.Profiles
{
    public class OrderLineProfile : Profile
    {
        public OrderLineProfile()
        {
            CreateMap<Entities.OrderLine, DTOs.OrderLineDto>().ReverseMap();
        }
    }
}
