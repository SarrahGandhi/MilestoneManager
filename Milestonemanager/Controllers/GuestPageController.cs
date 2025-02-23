using CoreEntityFramework.Services;
using Microsoft.AspNetCore.Mvc;
using Milestonemanager.Interfaces;
using Milestonemanager.Models;
using MilestoneManager.Interfaces;
using MilestoneManager.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MilestoneManager.Controllers
{
    public class GuestPageController : Controller

    {
        private readonly IGuestService _guestService;
        private readonly IEventGuestService _eventGuestService;
        private readonly IEventService _eventService;
        public GuestPageController(IGuestService guestService, IEventGuestService eventGuestService, IEventService eventService)
        {
            _guestService = guestService;
            _eventGuestService = eventGuestService;
            _eventService = eventService;
        }
        [HttpGet]
        public async Task<IActionResult> ListGuest()
        {
            var guest = await _guestService.GetGuests();
            var guestDtos = guest.Select(guest => new GuestDto()
            {
                GuestId = guest.GuestId,
                GuestName = guest.GuestName,
                GuestPhone = guest.GuestPhone,
                GuestAddress = guest.GuestAddress,
                GuestLocation = guest.GuestLocation,
                IsInvited = guest.IsInvited,
                GuestNotes = guest.GuestNotes,
                GuestCategory = guest.GuestCategory,




            }).ToList();
            return View(guestDtos);
        }
        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var eventGuest = await _eventGuestService.GetEventGuestByGuest(id);
            var guest = await _guestService.GetGuestById(id);
            var eventList = await _eventService.GetEvents();
            ViewData["EventGuest"] = eventGuest;
            ViewData["GuestName"] = guest.GuestName;
            ViewData["Guestnotes"] = guest.GuestNotes;
            ViewData["EventList"] = eventList.ToList();
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var eventList = await _eventService.GetEvents();
            ViewData["EventsList"] = eventList.ToList();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateGuest(Guest guest)
        {
            var filteredEventGuests = guest.EventGuests.Where(eg => eg.EventId != 0).ToList();

            GuestDto guestDto = new GuestDto()
            {
                GuestName = guest.GuestName,
                GuestLocation = guest.GuestLocation,
                GuestAddress = guest.GuestAddress,
                GuestPhone = guest.GuestPhone,
                IsInvited = guest.IsInvited,
                GuestCategory = guest.GuestCategory,
                GuestNotes = guest.GuestNotes
            };
            var response = await _guestService.AddGuest(guestDto);

            if (response.Status == ServiceResponse.ServiceStatus.Created)
            {
                foreach (var eventGuest in filteredEventGuests)
                {
                    var eventGuestDto = new EventGuestDto()
                    {
                        EventId = eventGuest.EventId,
                        GuestId = response.CreatedId,
                        IsRSVPAccepted = false,
                        EventMen = eventGuest.EventMen,
                        EventWomen = eventGuest.EventWomen,
                        EventKids = eventGuest.EventKids
                    };

                    var eventGuestResponse = await _eventGuestService.AddEventGuest(eventGuestDto);

                }
                return RedirectToAction("ListGuest", "GuestPage");
            }
            else
            {
                return RedirectToAction("Create", "GuestPage");
            }

        }
        // Show the edit form for an guest
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var eventList = await _eventService.GetEvents();
            ViewData["EventsList"] = eventList.ToList();
            var guest = await _guestService.GetGuestById(id);
            var eventGuest = await _eventGuestService.GetEventGuestByGuest(id);
            if (guest == null)
            {
                return NotFound();
            }
            var guestDto = new Guest()
            {
                GuestId = id,
                GuestName = guest.GuestName,
                GuestPhone = guest.GuestPhone,
                GuestAddress = guest.GuestAddress,
                GuestLocation = guest.GuestLocation,
                IsInvited = guest.IsInvited,
                GuestNotes = guest.GuestNotes,
                GuestCategory = guest.GuestCategory,
                EventGuests = eventGuest
            };
            return View(guestDto);
        }
        // Handle the update of an guest
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditGuest(Guest guest)
        {
            var guestDto = new GuestDto()
            {
                GuestId = guest.GuestId,
                GuestName = guest.GuestName,
                GuestLocation = guest.GuestLocation,
                GuestAddress = guest.GuestAddress,
                GuestPhone = guest.GuestPhone,
                GuestNotes = guest.GuestNotes,
                IsInvited = guest.IsInvited,
                GuestCategory = guest.GuestCategory
            };
            var response = await _guestService.UpdateGuest(guestDto);
            if (response.Status == ServiceResponse.ServiceStatus.Updated)
            {
                foreach (var eventGuest in guest.EventGuests)
                {
                    var eventGuestDto = new EventGuestDto()
                    {
                        GuestEventId = eventGuest.GuestEventId,
                        GuestId = guest.GuestId,
                        EventId = eventGuest.EventId,
                        EventMen = eventGuest.EventMen,
                        EventWomen = eventGuest.EventWomen,
                        EventKids = eventGuest.EventKids,
                        IsRSVPAccepted = eventGuest.IsRSVPAccepted
                    };
                    var eventGuestResponse = await _eventGuestService.UpdateEventGuest(eventGuestDto);
                }


                return RedirectToAction("ListGuest", "GuestPage");
            }
            else
            {
                return RedirectToAction("Edit", "GuestPage");
            }

        }
        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var guest = await _guestService.GetGuestById(id);
            var guestDto = new GuestDto()
            {
                GuestId = id,
                GuestName = guest.GuestName,
                GuestAddress = guest.GuestAddress,
                GuestLocation = guest.GuestLocation,
                GuestPhone = guest.GuestPhone,
                GuestNotes = guest.GuestNotes,
                IsInvited = guest.IsInvited,
                GuestCategory = guest.GuestCategory,



            };
            return View(guestDto);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteGuest(int id)
        {
            var response = await _guestService.DeleteGuest(id);
            if (response.Status == ServiceResponse.ServiceStatus.Deleted)
            {
                return RedirectToAction("ListGuest", "GuestPage");
            }
            else
            {
                return RedirectToAction("Delete", "GuestPage");
            }
        }
    }
}