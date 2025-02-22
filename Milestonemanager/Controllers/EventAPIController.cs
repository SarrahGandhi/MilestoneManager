using Microsoft.AspNetCore.Mvc;
using Milestonemanager.Models;
using MilestoneManager.Interfaces;
using MilestoneManager.Models;

namespace MilestoneManager.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EventAPIController : ControllerBase
    {
        private readonly IEventService _eventService;

        public EventAPIController(IEventService eventService)
        {
            _eventService = eventService;
        }

        /// <summary>
        /// Retrieves a list of all events available in the system.
        /// This method calls the event service to fetch all stored events.
        /// Returns an HTTP 200 response along with the list of events if successful.
        /// If no events are found, an empty list is returned.
        /// </summary>
        [HttpGet("Event")]
        public async Task<ActionResult<IEnumerable<Event>>> GetEvent()
        {
            IEnumerable<Event> event1 = await _eventService.GetEvents();
            return Ok(event1);
        }

        /// <summary>
        /// Retrieves an event by its unique identifier.
        /// Calls the event service to find an event matching the provided ID.
        /// Returns an HTTP 200 response with the event details if found.
        /// Returns an HTTP 404 response if no matching event exists.
        /// </summary>
        [HttpGet("GetEventById")]
        public async Task<ActionResult<Event>> FindEvent(int id)
        {
            var event1 = await _eventService.GetEventById(id);
            if (event1 == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(event1);
            }
        }

        /// <summary>
        /// Finds an event based on the given name.
        /// Queries the event service for an event with the specified name.
        /// Returns an HTTP 200 response with the event details if found.
        /// Returns an HTTP 404 response if no such event is found.
        /// </summary>
        [HttpGet("GetEventByName")]
        public async Task<ActionResult<Event>> FindEventByName(string name)
        {
            var event1 = await _eventService.GetEventByName(name);
            if (event1 == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(event1);
            }
        }

        /// <summary>
        /// Retrieves all events under a specified category.
        /// Calls the event service to get all events that match the given category.
        /// If events are found, an HTTP 200 response is returned with the event list.
        /// If no events match the category, an HTTP 404 response is returned.
        /// </summary>
        [HttpGet("GetEventsByCategory")]
        public async Task<ActionResult<List<Event>>> FindEventsByCategory(EventCategory category)
        {
            var events = await _eventService.GetEventsByCategory(category);

            if (events == null || !events.Any())
            {
                return NotFound();
            }
            else
            {
                return Ok(events);
            }
        }

        /// <summary>
        /// Retrieves events by location.
        /// This method fetches events based on the specified location string.
        /// Returns an HTTP 200 response with a list of events if found.
        /// Returns an HTTP 404 response if no matching events exist.
        /// </summary>
        [HttpGet("GetEventsByLocation")]
        public async Task<ActionResult<List<Event>>> FindEventsByLocation(string location)
        {
            var events = await _eventService.GetEventsByLocation(location);
            if (events == null || !events.Any())
            {
                return NotFound();
            }
            else
            {
                return Ok(events);
            }
        }

        /// <summary>
        /// Retrieves events scheduled for a specific date.
        /// Calls the event service to find events occurring on the given date.
        /// Returns an HTTP 200 response with a list of matching events.
        /// Returns an HTTP 404 response if no events are found for the specified date.
        /// </summary>
        [HttpGet("GetEventByDate")]
        public async Task<ActionResult<List<Event>>> FindEventsByDate(DateTime date)
        {
            var events = await _eventService.GetEventsByDate(date);
            if (events == null || !events.Any())
            {
                return NotFound();
            }
            else
            {
                return Ok(events);
            }
        }

        /// <summary>
        /// Adds a new event to the system.
        /// Accepts an event DTO containing event details.
        /// Returns an HTTP 201 response if creation is successful.
        /// Returns an HTTP 404 or 500 response in case of errors.
        /// </summary>
        [HttpPost("AddEvent")]
        public async Task<ActionResult<EventDto>> AddEvent(EventDto addevent)
        {
            ServiceResponse response = await _eventService.AddEvent(addevent);
            if (response.Status == ServiceResponse.ServiceStatus.NotFound)
            {
                return NotFound(response.Messages);
            }
            else if (response.Status == ServiceResponse.ServiceStatus.Error)
            {
                return StatusCode(500, response.Messages);
            }
            return Created($"api/Event/GetEventById/{response.CreatedId}", addevent);
        }

        /// <summary>
        /// Updates an existing event based on its ID.
        /// Accepts an event object containing updated details.
        /// Returns an HTTP 204 response if the update is successful.
        /// Returns an HTTP 400, 404, or 500 response in case of errors.
        /// </summary>
        [HttpPut("UpdateEvent/{id}")]
        public async Task<ActionResult> UpdateEvent(int id, EventDto updateevent)
        {
            if (id != updateevent.EventId)
            {
                return BadRequest();
            }
            ServiceResponse response = await _eventService.UpdateEvent(updateevent);
            if (response.Status == ServiceResponse.ServiceStatus.NotFound)
            {
                return NotFound(response.Messages);
            }
            else if (response.Status == ServiceResponse.ServiceStatus.Error)
            {
                return StatusCode(500, response.Messages);
            }
            return NoContent();
        }

        /// <summary>
        /// Deletes an event based on its ID.
        /// Calls the event service to remove the specified event from the database.
        /// Returns an HTTP 200 response if deletion is successful.
        /// Returns an HTTP 404 or 500 response in case of errors.
        /// </summary>
        [HttpDelete("DeleteEvent/{id}")]
        public async Task<ActionResult<Event>> DeleteEvent(int id)
        {
            ServiceResponse response = await _eventService.DeleteEvent(id);
            if (response.Status == ServiceResponse.ServiceStatus.NotFound)
            {
                return NotFound(response.Messages);
            }
            else if (response.Status == ServiceResponse.ServiceStatus.Error)
            {
                return StatusCode(500, response.Messages);
            }
            return Ok(response.Messages);
        }
    }
}
