﻿using AutoMapper;
using BookMyEvent.Grpc;
using Google.Protobuf.WellKnownTypes;

namespace BookMyEvent.Services.EventCatalog.Profiles
{
    public class EventProfile : Profile
    {
        public EventProfile()
        {
            CreateMap<Entities.Event, DTOs.EventDTO>()
             .ForMember(dest => dest.CategoryName, opts => opts.MapFrom(src => src.Category.Name))
             .ForMember(dest => dest.VenueName, opts => opts.MapFrom(src => src.Venue.Name))
             .ForMember(dest => dest.VenueCity, opts => opts.MapFrom(src => src.Venue.City))
             .ForMember(dest => dest.VenueCountry, opts => opts.MapFrom(src => src.Venue.Country));
          
            CreateMap<Entities.Event, Event>()
                 .ForMember(dest => dest.CategoryName, opts => opts.MapFrom(src => src.Category.Name))
                .ForMember(dest => dest.Date, opts => opts.MapFrom(src => src.Date.ToUniversalTime().ToTimestamp()));
        }
    }
}
