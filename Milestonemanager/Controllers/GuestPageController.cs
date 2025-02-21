using Microsoft.AspNetCore.Mvc;
using MilestoneManager.Interfaces;
using MilestoneManager.Models;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Milestonemanager.Models;

namespace MilestoneManager.Controllers
{
    public class GuestPageController : Controller
    {
        private readonly IGuestService _guestService; // Inject the service
        public GuestPageController(IGuestService guestService) // Constructor
        {
            _guestService = guestService; // Assign the service to the controller

        }
        public IActionResult Index()
        {
            return RedirectToAction("ListGuest");
        }
        public async Task<IActionResult> ListGuest()
        {
            IEnumerable<Guest> guests = await _guestService.GetGuests();
            IEnumerable<GuestDto> guestDtos = guests.Select(guest => new GuestDto // Convert Guest to GuestDto
            {
                GuestId = guest.GuestId, // Ensure GuestId is included for editing
                GuestName = guest.GuestName,
                GuestCategory = guest.GuestCategory,
                IsInvited = guest.IsInvited
            });

            return View(guestDtos);
        }
        [HttpGet]
        public async Task<IActionResult> EditGuest(int id)
        {
            var guest = await _guestService.GetGuestById(id); // Get Guest entity
            if (guest == null) // If the guest doesn't exist
            {
                return NotFound();
            }

            // Convert Guest to GuestDto
            var guestDto = new GuestDto
            {
                GuestId = guest.GuestId,
                GuestName = guest.GuestName,
                GuestLocation = guest.GuestLocation,
                IsInvited = guest.IsInvited,
                GuestPhone = guest.GuestPhone,
                GuestCategory = guest.GuestCategory
            };

            return View(guestDto);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditGuest(GuestDto model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var updatedGuest = new Guest
            {
                GuestId = model.GuestId,
                GuestName = model.GuestName,
                GuestLocation = model.GuestLocation,
                IsInvited = model.IsInvited,
                GuestPhone = model.GuestPhone,
                GuestCategory = model.GuestCategory
            };

            var result = await _guestService.UpdateGuest(updatedGuest);

            if (result.Success) // Assuming UpdateGuest returns a ServiceResponse
            {
                return RedirectToAction("ListGuest");
            }

            ModelState.AddModelError("", "Failed to update guest. Please try again.");
            return View(model);
        }
    }
}