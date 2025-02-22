using Microsoft.AspNetCore.Mvc;
using Milestonemanager.Models;
using MilestoneManager.Interfaces;
using MilestoneManager.Models;

namespace MilestoneManager.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GuestAPIController : ControllerBase
    {
        private readonly IGuestService _guestService;
        public GuestAPIController(IGuestService guestService)
        {
            _guestService = guestService;
        }
        /// <summary>
        /// Retrieves a list of all guests stored in the system.  
        /// This method asynchronously calls the guest service to fetch all available guests.  
        /// It returns an `IEnumerable<Guest>` wrapped in an `ActionResult`.  
        /// If successful, it responds with an HTTP 200 status along with the list of guests.  
        /// If there are no guests, it still returns an empty list rather than an error.  
        /// This method does not require any parameters.
        /// </summary>
        [HttpGet("Guest")]
        public async Task<ActionResult<IEnumerable<Guest>>> GetGuest()
        {
            IEnumerable<Guest> guest = await _guestService.GetGuests();
            return Ok(guest);
        }

        /// <summary>
        /// Fetches a specific guest by their unique ID.  
        /// The method takes an integer `id` as a parameter and queries the service for a matching guest.  
        /// If a guest is found, it returns an HTTP 200 response with the guest details.  
        /// If no guest matches the provided ID, it returns an HTTP 404 Not Found response.  
        /// This helps ensure that only valid guest records are accessed in the system.  
        /// The method is useful for retrieving guest details in a detailed view.
        /// </summary>
        [HttpGet("GetGuestById")]
        public async Task<ActionResult<Guest>> FindGuest(int id)
        {
            var guest = await _guestService.GetGuestById(id);
            if (guest == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(guest);
            }
        }
        /// <summary>
        /// Searches for a guest based on their name.  
        /// It accepts a `string` parameter representing the guest’s name and queries the service layer.  
        /// If a matching guest is found, it returns an HTTP 200 response with the guest object.  
        /// If no matching name is found, an HTTP 404 Not Found response is returned.  
        /// This method helps in scenarios where users need to find guests by their names instead of IDs.  
        /// It is case-sensitive and requires an exact match unless modified to handle variations.
        /// </summary>

        [HttpGet("GetGuestByName")]
        public async Task<ActionResult<Guest>> FindGuestByName(string name)
        {
            var guest = await _guestService.GetGuestByName(name);
            if (guest == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(guest);
            }
        }
        /// <summary>
        /// Retrieves a list of guests based on their assigned category.  
        /// The method takes a `GuestCategory` enum as a parameter to filter guests accordingly.  
        /// It queries the guest service and returns a list of matching guests, wrapped in an HTTP 200 response.  
        /// If no guests are found in the specified category, an HTTP 404 Not Found response is returned.  
        /// This method helps in segmenting guests into different categories for better organization.  
        /// Useful for event planners who need to sort guests based on roles like VIPs or general attendees.
        /// </summary>
        [HttpGet("GetGuestsByCategory")]
        public async Task<ActionResult<List<Guest>>> FindGuestsByCategory(GuestCategory category)
        {
            var guests = await _guestService.GetGuestsByCategory(category);

            if (guests == null || !guests.Any())  // Check if the list is empty or null
            {
                return NotFound();
            }
            else
            {
                return Ok(guests);
            }
        }
        /// <summary>
        /// Adds a new guest to the system using the provided data.  
        /// The method accepts a `GuestDto` object containing details like name, category, and contact information.  
        /// If the operation is successful, it returns an HTTP 201 Created response with the guest’s ID.  
        /// If the provided data is invalid or an internal error occurs, it responds with an appropriate HTTP error code.  
        /// The `ServiceResponse` object ensures that status messages and IDs are properly communicated.  
        /// This method facilitates the addition of guests into the event management system.
        /// </summary>

        [HttpPost("AddGuest")]
        public async Task<ActionResult<GuestDto>> AddGuest(GuestDto addguest)
        {
            ServiceResponse response = await _guestService.AddGuest(addguest);
            if (response.Status == ServiceResponse.ServiceStatus.NotFound)
            {
                return NotFound(response.Messages);
            }
            else if (response.Status == ServiceResponse.ServiceStatus.Error)
            {
                return StatusCode(500, response.Messages);
            }
            return Created($"api/Guest/GetGuestById/{response.CreatedId}", addguest);
        }
        /// <summary>
        /// Updates an existing guest's details in the system.  
        /// It requires the guest ID as a URL parameter and the updated `Guest` object in the request body.  
        /// If the ID in the URL does not match the one in the object, it returns an HTTP 400 Bad Request response.  
        /// If the guest does not exist, an HTTP 404 Not Found response is returned.  
        /// On successful update, the method returns an HTTP 204 No Content response.  
        /// This ensures that modifications to guest details are properly validated and processed.
        /// </summary>
        [HttpPut("UpdateGuest/{id}")]
        public async Task<ActionResult> UpdateGuest(int id, GuestDto updateguest)
        {
            if (id != updateguest.GuestId)
            {
                return BadRequest();
            }
            ServiceResponse response = await _guestService.UpdateGuest(updateguest);
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
        /// Deletes a guest from the system based on their unique ID.  
        /// It accepts an integer `id` as a parameter and attempts to remove the corresponding guest record.  
        /// If the guest exists, it is deleted, and an HTTP 200 OK response with a confirmation message is returned.  
        /// If the guest does not exist, an HTTP 404 Not Found response is returned.  
        /// Any unexpected issues, such as database errors, result in an HTTP 500 Internal Server Error response.  
        /// This method ensures proper deletion while handling errors gracefully.
        /// </summary>
        [HttpDelete("DeleteGuest/{id}")]
        public async Task<ActionResult<Guest>> DeleteGuest(int id)
        {
            ServiceResponse response = await _guestService.DeleteGuest(id);
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