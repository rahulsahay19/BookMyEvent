using AutoMapper;
using BookMyEvent.Grpc;
using BookMyEvent.Services.EventCatalog.DTOs;


namespace BookMyEvent.Services.EventCatalog.Profiles
{
    public class CategoryProfile : Profile
    {
        public CategoryProfile()
        {
            CreateMap<Entities.Category, CategoryDTO>().ReverseMap();
            CreateMap<Entities.Category, Category>().ReverseMap();
        }
    }
}
