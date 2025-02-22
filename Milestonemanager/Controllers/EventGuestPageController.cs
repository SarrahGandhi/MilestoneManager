using Microsoft.AspNetCore.Mvc;
using Milestonemanager.Interfaces;
using Milestonemanager.Models;
using MilestoneManager.Interfaces;
using MilestoneManager.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MilestoneManager.Controllers
{
    public class EventGuestPageController : Controller
    {
        private readonly IEventGuestService _eventguestservice;
        public EventGuestPageController(IEventGuestService eventGuestService)
        {
            _eventguestservice = eventGuestService;
        }
        [HttpGet]
        public async Task<IActionResult> ListEventGuest()
        {
            var eventGuest = await _eventguestservice.GetEventGuests();
            var eventGuestDto = eventGuest.Select(eventGuest => new EventGuestDto
            {
                GuestEventId = eventGuest.GuestEventId,
                EventId = eventGuest.EventId,
                EventMen = eventGuest.EventMen,
                EventWomen = eventGuest.EventWomen,
                EventKids = eventGuest.EventKids,
                IsRSVPAccepted = eventGuest.IsRSVPAccepted,
                GuestId = eventGuest.GuestId,

            }).ToList();
            return View(eventGuestDto);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateEventGuest(EventGuestDto eventGuestDto)
        {
            var response = await _eventguestservice.AddEventGuest(eventGuestDto);
            if (response.Status == ServiceResponse.ServiceStatus.Created)
            {
                return RedirectToAction("ListEventGuest", "EventGuestPage");
            }
            else
            {
                return RedirectToAction("Create", "EventGuestPage");
            }
        }
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var eventGuest = await _eventguestservice.GetEventGuestById(id);
            if (eventGuest == null)
            {
                return NotFound();
            }
            var eventGuestDto = new EventGuestDto
            {
                GuestEventId = id,
                EventId = eventGuest.EventId,
                EventMen = eventGuest.EventMen,
                EventWomen = eventGuest.EventWomen,
                EventKids = eventGuest.EventKids,
                IsRSVPAccepted = eventGuest.IsRSVPAccepted,
                GuestId = eventGuest.GuestId
            };
            return View(eventGuestDto);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditEventGuest(EventGuestDto eventGuestDto)
        {
            var response = await _eventguestservice.UpdateEventGuest(eventGuestDto);
            if (response.Status == ServiceResponse.ServiceStatus.Updated)
            {
                return RedirectToAction("ListEventGuest", "EventGuestPage");
            }
            else
            {
                return RedirectToAction("Edit", "EventGuestPage");
            }
        }
        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var eventGuest = await _eventguestservice.GetEventGuestById(id);
            var eventGuestDto = new EventGuestDto
            {
                GuestEventId = id,
                EventId = eventGuest.EventId,
                EventMen = eventGuest.EventMen,
                EventWomen = eventGuest.EventWomen,
                EventKids = eventGuest.EventKids,
                IsRSVPAccepted = eventGuest.IsRSVPAccepted,
                GuestId = eventGuest.GuestId
            };
            return View(eventGuestDto);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteEventGuest(int id)
        {
            var response = await _eventguestservice.DeleteEventGuest(id);
            if (response.Status == ServiceResponse.ServiceStatus.Deleted)
            {
                return RedirectToAction("ListEventGuest", "EventGuestPage");
            }
            else
            {
                return RedirectToAction("Delete", "EventGuestPage");
            }
        }

    }
}