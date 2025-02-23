using Microsoft.AspNetCore.Mvc;
using Milestonemanager.Interfaces;
using Milestonemanager.Models;
using MilestoneManager.Interfaces;
using MilestoneManager.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MilestoneManager.Controllers
{
    /// <summary>
    /// API Controller for managing Event Guests.
    /// Provides endpoints for retrieving, adding, updating, and deleting event guests.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class EventGuestController : ControllerBase
    {
        private readonly IEventGuestService _eventGuestService;
        /// <summary>
        /// Initializes a new instance of the <see cref="EventGuestController"/> class.
        /// </summary>
        /// <param name="eventGuestService">Service for managing event guests.</param>
        public EventGuestController(IEventGuestService eventGuestService)
        {
            _eventGuestService = eventGuestService;
        }
        /// <summary>
        /// Retrieves all event guests.
        /// </summary>
        /// <returns>A list of event guests.</returns>
        [HttpGet("EventGuest")]
        public async Task<ActionResult<IEnumerable<EventGuest>>> GetEventGuests()

        {
            IEnumerable<EventGuest> eventGuest = await _eventGuestService.GetEventGuests();
            return Ok(eventGuest);
        }
        /// <summary>
        /// Adds a new event guest.
        /// </summary>
        /// <param name="eventGuest">DTO containing event guest details.</param>
        /// <returns>The created event guest along with its location.</returns>
        [HttpPost("AddEventGuest")]
        public async Task<ActionResult<EventGuest>> AddEventGuest(EventGuestDto eventGuest)
        {
            ServiceResponse response = await _eventGuestService.AddEventGuest(eventGuest);
            if (response.Status == ServiceResponse.ServiceStatus.NotFound)
            {
                return NotFound(response.Messages);
            }
            else if (response.Status == ServiceResponse.ServiceStatus.Error)
            {
                return StatusCode(500, response.Messages);
            }
            eventGuest.GuestEventId = response.CreatedId;
            return Created($"/api/EventGuest/GetEventGuestById/{response.CreatedId}", eventGuest);
        }
        /// <summary>
        /// Updates an existing event guest.
        /// </summary>
        /// <param name="id">The ID of the event guest to update.</param>
        /// <param name="eventGuest">Updated event guest details.</param>
        /// <returns>No content if update is successful, otherwise appropriate error response.</returns>


        [HttpPut("UpdateEventGuest/{id}")]
        public async Task<ActionResult<EventGuest>> UpdateEventGuest(int id, EventGuestDto eventGuest)
        {
            if (id != eventGuest.GuestEventId)
            {
                return BadRequest();
            }
            ServiceResponse response = await _eventGuestService.UpdateEventGuest(eventGuest);
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
        /// Deletes an event guest by ID.
        /// </summary>
        /// <param name="id">The ID of the event guest to delete.</param>
        /// <returns>OK response with confirmation message or an appropriate error response.</returns>

        [HttpDelete("DeleteAdmin/{id}")]
        public async Task<ActionResult<EventGuest>> DeleteEventGuest(int id)
        {
            ServiceResponse response = await _eventGuestService.DeleteEventGuest(id);
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
