using AutoMapper;
using BookMyEvent.Services.Discount.DTOs;
using BookMyEvent.Services.Discount.Entities;

namespace BookMyEvent.Services.Discount.Profiles
{
    public class CouponProfile : Profile
    {
        public CouponProfile()
        {
            CreateMap<Coupon, CouponDTO>().ReverseMap();
        }
    }
}
