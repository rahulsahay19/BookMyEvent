using AutoMapper;
using BookMyEvent.Services.ShoppingCart.DTOs;
using BookMyEvent.Services.ShoppingCart.Entities;

namespace BookMyEvent.Services.ShoppingCart.Profiles
{
    public class BasketChangeEventProfile : Profile
    {
        public BasketChangeEventProfile()
        {
            CreateMap<BasketChangeEvent, BasketChangeEventForPublication>().ReverseMap();
        }
    }
}
