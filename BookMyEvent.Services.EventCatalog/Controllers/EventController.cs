using AutoMapper;
using BookMyEvent.Integration.MessagingBus;
using BookMyEvent.Services.EventCatalog.DTOs;
using BookMyEvent.Services.EventCatalog.Entities;
using BookMyEvent.Services.EventCatalog.Messages;
using BookMyEvent.Services.EventCatalog.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BookMyEvent.Services.EventCatalog.Controllers
{
    [Route("api/events")]
    [ApiController]
   // [Authorize(Policy = "CanRead")]
    public class EventController : ControllerBase
    {
        //private readonly IEventRepository eventRepository;
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        private readonly IMessageBus messageBus;

        public EventController(IUnitOfWork unitOfWork, IMapper mapper, IMessageBus messageBus)
        {
            //this.eventRepository = eventRepository;
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
            this.messageBus = messageBus;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<DTOs.EventDTO>>> Get(
            [FromQuery] Guid categoryId)
        {
            var result = await unitOfWork.EventRepository.GetEvents(categoryId);
            return Ok(mapper.Map<List<DTOs.EventDTO>>(result));
        }

        [HttpGet("{eventId}")]
        public async Task<ActionResult<DTOs.EventDTO>> GetById(Guid eventId)
        {
            var result = await unitOfWork.EventRepository.GetEventById(eventId);
            return Ok(mapper.Map<DTOs.EventDTO>(result));
        }

        [HttpPost("eventupdate")]
        public async Task<ActionResult<EventUpdate>> Post(EventUpdate eventUpdate)
        {
            var eventToUpdate = await unitOfWork.EventRepository.GetEventById(eventUpdate.EventId);

            eventToUpdate.Name = eventUpdate.Name;
            eventToUpdate.Price = eventUpdate.Price;
            eventToUpdate.Date = eventUpdate.Date;
            // message property is only for the message to other microservices

            // structure message to be sent to service bus
            EventUpdatedMessage eventUpdatedMessage = new EventUpdatedMessage
            {
                Id = Guid.NewGuid(),
                CreationDateTime = DateTime.Now,
                EventId = eventUpdate.EventId,
                Name = eventUpdate.Name,
                Date = eventUpdate.Date,
                Price = eventUpdate.Price,
                Message = eventUpdate.Message
            };

            // serialize message for storage in database table text field
            var jsonMessage = JsonConvert.SerializeObject(eventUpdatedMessage);

            IntegrationEventLog logEntry = new IntegrationEventLog
            {
                IntegrationEventType = "TicketedEventChange",
                ServiceBusTopicName = "eventupdatedmessage",
                IntegrationEventBody = jsonMessage,
                State = "Pending"
            };

            unitOfWork.IntegrationEventLogRepository.AddEventLogEntry(logEntry);

            try
            {
                unitOfWork.Commit();

                // await messageBus.PublishMessage(eventUpdatedMessage, "eventupdatedmessage");
            }
            catch (Exception e)
            {
                unitOfWork.Rollback();

                Console.WriteLine(e);
                throw;
            }

            return Ok(eventUpdate);
        }

    }
}