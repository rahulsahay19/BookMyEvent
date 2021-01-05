using AutoMapper;

namespace BookMyEvent.Services.Ordering.Profiles
{
    public class OrderProfile : Profile
    {
        public OrderProfile()
        {
            CreateMap<Entities.Order, DTOs.OrderDto>().ReverseMap();
        }
    }
}
