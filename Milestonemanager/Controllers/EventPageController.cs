using Microsoft.AspNetCore.Mvc;
using MilestoneManager.Models;
using MilestoneManager.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Milestonemanager.Models;

namespace MilestoneManager.Controllers
{
    public class EventPageController : Controller
    {
        private readonly IEventService _eventService;

        public EventPageController(IEventService eventService)
        {
            _eventService = eventService;
        }

        public IActionResult Index()
        {
            return RedirectToAction("ListEvent");
        }

        public async Task<IActionResult> ListEvent()
        {
            IEnumerable<Event> events = await _eventService.GetEvents();
            IEnumerable<EventDto> eventDtos = events.Select(eventItem => new EventDto
            {
                EventName = eventItem.EventName,  // Fixed: Added commas
                EventDate = eventItem.EventDate,
                EventLocation = eventItem.EventLocation,
                EventCategory = eventItem.EventCategory
            });

            return View(eventDtos);
        }
        public async Task<IActionResult> EditEvent(int id)
        {
            var eventItem = await _eventService.GetEventById(id);
            if (eventItem == null)
            {
                return NotFound();
            }

            var eventDto = new EventDto
            {
                EventId = eventItem.EventId,
                EventName = eventItem.EventName,
                EventDate = eventItem.EventDate,
                EventLocation = eventItem.EventLocation,
                EventCategory = eventItem.EventCategory
            };

            return View(eventDto);
        }

        [HttpPost]
        public async Task<IActionResult> EditEvent(EventDto model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var updatedEvent = new Event
            {
                EventId = model.EventId,
                EventName = model.EventName,
                EventDate = model.EventDate,
                EventLocation = model.EventLocation,
                EventCategory = model.EventCategory
            };

            await _eventService.UpdateEvent(updatedEvent);

            return RedirectToAction("ListEvent");
        }
        public async Task<IActionResult> DeleteEvent(int id)
        {
            var eventToDelete = await _eventService.GetEventById(id);
            if (eventToDelete == null)
            {
                return NotFound();
            }

            return View(eventToDelete); // This will open a confirmation view
        }

        [HttpPost, ActionName("DeleteEvent")]
        public async Task<IActionResult> ConfirmDelete(int id)
        {
            var eventToDelete = await _eventService.GetEventById(id);
            if (eventToDelete == null)
            {
                return NotFound();
            }

            await _eventService.DeleteEvent(id);
            return RedirectToAction(nameof(ListEvent));
        }

    }
}

