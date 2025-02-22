using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Milestonemanager.Interfaces;
using Milestonemanager.Models;
using MilestoneManager.Interfaces;
using MilestoneManager.Models;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace MilestoneManager.Controllers
{
    public class EventTaskPageController : Controller
    {
        private readonly IEventTaskService _eventTaskService;
        private readonly IAdminService _adminService;
        private readonly IEventService _eventService;

        public EventTaskPageController(IEventTaskService eventTaskService, IAdminService adminService, IEventService eventService)
        {
            _eventTaskService = eventTaskService;
            _adminService = adminService;
            _eventService = eventService;
        }
        [HttpGet]
        public async Task<IActionResult> ListEventTask()
        {
            var tasks = await _eventTaskService.GetEventTasks();
            var admin = await _adminService.GetAdmins();
            var eventList = await _eventService.GetEvents();
            var taskDtos = tasks.Select(task => new EventTaskListDto
            {

                TaskId = task.TaskId,
                TaskName = task.TaskName,
                AdminName = admin.FirstOrDefault(item => item.AdminId == task.AdminId)?.AdminName ?? "No Admin Assigned",
                EventName = eventList.FirstOrDefault(item => item.EventId == task.EventId)?.EventName ?? "No Event Assigned",
                TaskDescription = task.TaskDescription,
                DueDate = task.DueDate,
                IsCompleted = task.IsCompleted,
                EventTaskCategory = task.EventTaskCategory
            }).ToList();


            return View(taskDtos);
        }
        // [HttpGet]
        // public async Task<IActionResult>Details(int id)
        // {
        //     var task = await _eventTaskService.GetEventTaskById(id);
        //     var admin = await _adminService.Get();
        //     ViewData["AdminName"] = admin.AdminName;
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var adminList = await _adminService.GetAdmins();
            var eventList = await _eventService.GetEvents();

            if (adminList == null || eventList == null)
            {
                return NotFound();
            }


            ViewData["Admins"] = adminList.ToList();
            ViewData["Events"] = eventList.ToList();
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateEventTask(EventTaskDto eventTaskDto)
        {
            var response = await _eventTaskService.AddEventTask(eventTaskDto);
            if (response.Status == ServiceResponse.ServiceStatus.Created)
            {
                return RedirectToAction("ListEventTask", "EventTaskPage");
            }
            else
            {
                return RedirectToAction("Create", "EventTaskPage");
            }

        }
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var eventTask = await _eventTaskService.GetEventTaskById(id);
            var adminList = await _adminService.GetAdmins();
            var eventList = await _eventService.GetEvents();

            if (adminList == null || eventList == null || eventTask == null)
            {
                return NotFound();
            }


            ViewData["Admins"] = adminList.ToList();
            ViewData["Events"] = eventList.ToList();
            var eventTaskDto = new EventTaskDto
            {
                TaskId = id,
                TaskName = eventTask.TaskName,
                TaskDescription = eventTask.TaskDescription,
                DueDate = eventTask.DueDate,
                EventId = eventTask.EventId,
                AdminId = eventTask.AdminId,
                IsCompleted = eventTask.IsCompleted,
                EventTaskCategory = eventTask.EventTaskCategory
            };
            return View(eventTaskDto);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditEventTask(EventTaskDto eventTaskDto)
        {
            var response = await _eventTaskService.UpdateEventTask(eventTaskDto);
            if (response.Status == ServiceResponse.ServiceStatus.Updated)
            {
                return RedirectToAction("ListEventTask", "EventTaskPage");
            }
            else
            {
                return RedirectToAction("Edit", "EventTaskPage");
            }
        }
        public async Task<IActionResult> Delete(int id)
        {
            var eventTask = await _eventTaskService.GetEventTaskById(id);
            var eventTaskDto = new EventTaskDto
            {
                TaskId = id,
                TaskName = eventTask.TaskName,
                TaskDescription = eventTask.TaskDescription,
                DueDate = eventTask.DueDate,
                EventId = eventTask.EventId,
                AdminId = eventTask.AdminId,
                IsCompleted = eventTask.IsCompleted,
                EventTaskCategory = eventTask.EventTaskCategory
            };
            return View(eventTaskDto);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteEventTask(int id)
        {
            var response = await _eventTaskService.DeleteEventTask(id);
            if (response.Status == ServiceResponse.ServiceStatus.Deleted)
            {
                return RedirectToAction("ListEventTask", "EventTaskPage");
            }
            else
            {
                return RedirectToAction("Delete", "EventTaskPage");
            }
        }
    }
}

