using Microsoft.AspNetCore.Mvc;
using Milestonemanager.Interfaces;
using Milestonemanager.Models;
using MilestoneManager.Interfaces;
using MilestoneManager.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MilestoneManager.Controllers
{
    public class EventPageController : Controller
    {
        private readonly IEventService _eventService;
        private readonly IEventTaskService _taskService;

        public EventPageController(IEventService eventService, IEventTaskService taskService)
        {
            _eventService = eventService;
            _taskService = taskService;
        }

        public IActionResult Index()
        {
            return RedirectToAction("ListEvent");
        }

        public async Task<IActionResult> ListEvent()
        {
            var events = await _eventService.GetEvents();
            var eventDtos = events.Select(eventItem => new EventDto
            {
                EventId = eventItem.EventId,
                EventName = eventItem.EventName,
                EventDate = eventItem.EventDate,
                EventLocation = eventItem.EventLocation,
                EventCategory = eventItem.EventCategory
            });
            return View(eventDtos);

        }
        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var tasks = await _taskService.GetEventTasksByEventId(id);
            var event1 = await _eventService.GetEventById(id);
            ViewData["EventName"] = event1.EventName;
            ViewData["Tasks"] = tasks;
            return View();
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken()]
        public async Task<IActionResult> CreateEvent(EventDto eventDto)
        {
            var response = await _eventService.AddEvent(eventDto);
            if (response.Status == ServiceResponse.ServiceStatus.Created)

            {
                return RedirectToAction("ListEvent", "EventPage");
            }
            else
            {
                return RedirectToAction("Create", "EventPage");
            }
        }
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var event1 = await _eventService.GetEventById(id);
            if (event1 == null)
            {
                return NotFound();
            }
            var eventDto = new EventDto
            {
                EventId = id,
                EventName = event1.EventName,
                EventDate = event1.EventDate,
                EventLocation = event1.EventLocation,
                EventCategory = event1.EventCategory
            };
            return View(eventDto);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditEvent(EventDto eventDto)
        {
            var response = await _eventService.UpdateEvent(eventDto);
            if (response.Status == ServiceResponse.ServiceStatus.Updated)
            {
                return RedirectToAction("ListEvent", "EventPage");
            }
            else
            {
                return RedirectToAction("Edit", "EventPage");
            }
        }
        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var event1 = await _eventService.GetEventById(id);
            var eventDto = new EventDto
            {
                EventId = id,
                EventName = event1.EventName,
                EventDate = event1.EventDate,
                EventLocation = event1.EventLocation,
                EventCategory = event1.EventCategory
            };
            return View(eventDto);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteEvent(int id)
        {
            var response = await _eventService.DeleteEvent(id);
            if (response.Status == ServiceResponse.ServiceStatus.Deleted)
            {
                return RedirectToAction("ListEvent", "EventPage");
            }
            else
            {
                return RedirectToAction("Delete", "EventPage");
            }
        }


    }
}