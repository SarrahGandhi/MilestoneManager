using Microsoft.AspNetCore.Mvc;
using Milestonemanager.Interfaces;
using Milestonemanager.Models;
using MilestoneManager.Interfaces;
using MilestoneManager.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MilestoneManager.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EventGuestController : ControllerBase
    {
        private readonly IEventGuestService _eventGuestService;
        public EventGuestController(IEventGuestService eventGuestService)
        {
            _eventGuestService = eventGuestService;
        }
        [HttpGet("EventGuest")]
        public async Task<ActionResult<IEnumerable<EventGuest>>> GetEventGuests()

        {
            IEnumerable<EventGuest> eventGuest = await _eventGuestService.GetEventGuests();
            return Ok(eventGuest);
        }
        [HttpPost("AddEventGuest")]
        public async Task<ActionResult<EventGuestDto>> AddEventGuest(EventGuestDto eventGuest)
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
