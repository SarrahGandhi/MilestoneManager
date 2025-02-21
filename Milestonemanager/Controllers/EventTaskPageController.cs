using Microsoft.AspNetCore.Mvc;
using Milestonemanager.Models;
using MilestoneManager.Interfaces;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;

namespace MilestoneManager.Controllers
{
    public class EventTaskPageController : Controller
    {
        private readonly IEventTaskService _eventTaskService;

        public EventTaskPageController(IEventTaskService eventTaskService)
        {
            _eventTaskService = eventTaskService;
        }

        public IActionResult Index()
        {
            return RedirectToAction("ListEventTask");
        }

        public async Task<IActionResult> ListEventTask()
        {
            IEnumerable<EventTask> eventTasks = await _eventTaskService.GetEventTasks();
            IEnumerable<EventTaskDto> eventTaskDtos = eventTasks.Select(eventTask => new EventTaskDto
            {
                TaskId = eventTask.TaskId,
                TaskDescription = eventTask.TaskDescription,
                EventId = eventTask.EventId,
                DueDate = eventTask.DueDate,
                IsCompleted = eventTask.IsCompleted,
                EventTaskCategory = eventTask.EventTaskCategory
            });

            return View(eventTaskDtos);
        }

        [HttpGet]
        public async Task<IActionResult> EditEventTask(int id)
        {
            var eventTask = await _eventTaskService.GetEventTaskById(id);
            if (eventTask == null)
            {
                return NotFound();
            }

            var eventTaskDto = new EventTaskDto
            {
                TaskId = eventTask.TaskId,
                TaskDescription = eventTask.TaskDescription,
                EventId = eventTask.EventId,
                DueDate = eventTask.DueDate,
                IsCompleted = eventTask.IsCompleted,
                EventTaskCategory = eventTask.EventTaskCategory
            };

            return View(eventTaskDto);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditEventTask(EventTaskDto model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var updatedEventTask = new EventTask
            {
                TaskId = model.TaskId,
                TaskDescription = model.TaskDescription,
                EventId = model.EventId,
                DueDate = model.DueDate,
                IsCompleted = model.IsCompleted,
                EventTaskCategory = model.EventTaskCategory
            };

            var result = await _eventTaskService.UpdateEventTask(updatedEventTask);
            if (result.Success)
            {
                return RedirectToAction("ListEventTask");
            }

            ModelState.AddModelError("", "Failed to update event task. Please try again.");
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteEventTask(int id)
        {
            var result = await _eventTaskService.DeleteEventTask(id);
            if (result.Success)
            {
                return RedirectToAction("ListEventTask");
            }

            ModelState.AddModelError("", "Failed to delete event task.");
            return RedirectToAction("ListEventTask");
        }
    }
}
