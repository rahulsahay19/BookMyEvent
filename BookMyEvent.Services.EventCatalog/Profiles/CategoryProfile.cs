using AutoMapper;
using BookMyEvent.Services.EventCatalog.DTOs;
using BookMyEvent.Services.EventCatalog.Entities;

namespace BookMyEvent.Services.EventCatalog.Profiles
{
    public class CategoryProfile : Profile
    {
        public CategoryProfile()
        {
            CreateMap<Category, CategoryDTO>().ReverseMap();
        }
    }
}
