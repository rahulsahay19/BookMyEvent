using AutoMapper;
using BookMyEvent.Integration.MessagingBus;
using BookMyEvent.Services.EventCatalog.DTOs;
using BookMyEvent.Services.EventCatalog.Messages;
using BookMyEvent.Services.EventCatalog.Repositories;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BookMyEvent.Services.EventCatalog.Controllers
{
    [Route("api/events")]
    [ApiController]
    public class EventController : ControllerBase
    {
        private readonly IEventRepository _eventRepository;
        private readonly IMapper _mapper;
        private readonly IMessageBus _messageBus;

        public EventController(IEventRepository eventRepository, IMapper mapper, IMessageBus messageBus)
        {
            _eventRepository = eventRepository;
            _mapper = mapper;
            _messageBus = messageBus;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<EventDTO>>> Get(
            [FromQuery] Guid categoryId)
        {
            var result = await _eventRepository.GetEvents(categoryId);
            return Ok(_mapper.Map<List<EventDTO>>(result));
        }

        [HttpGet("{eventId}")]
        public async Task<ActionResult<EventDTO>> GetById(Guid eventId)
        {
            var result = await _eventRepository.GetEventById(eventId);
            return Ok(_mapper.Map<EventDTO>(result));
        }

        [HttpPost("eventpriceupdate")]
        public async Task<ActionResult<PriceUpdate>> Post(PriceUpdate priceUpdate)
        {
            var eventToUpdate = await _eventRepository.GetEventById(priceUpdate.EventId);
            eventToUpdate.Price = priceUpdate.Price;
            await _eventRepository.SaveChanges();

            //send integration event on to service bus

            PriceUpdatedMessage priceUpdatedMessage = new PriceUpdatedMessage
            {
                EventId = priceUpdate.EventId,
                Price = priceUpdate.Price
            };

            try
            {
                await _messageBus.PublishMessage(priceUpdatedMessage, "priceupdatedmessage");
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }


            return Ok(priceUpdate);
        }
    }
}