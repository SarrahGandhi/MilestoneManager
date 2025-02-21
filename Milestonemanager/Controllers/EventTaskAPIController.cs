using Microsoft.AspNetCore.Mvc;
using Milestonemanager.Models;
using MilestoneManager.Interfaces;
using MilestoneManager.Models;

namespace MilestoneManager.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EventTaskAPIController : ControllerBase
    {
        private readonly IEventTaskService _eventTaskService;
        public EventTaskAPIController(IEventTaskService eventTaskService)
        {
            _eventTaskService = eventTaskService;
        }

        /// <summary>
        /// Retrieves all event tasks from the system.
        /// This method calls the event task service to fetch a collection of all event tasks.
        /// If there are no tasks available, an empty list is returned.
        /// The method returns an HTTP 200 status code upon successful retrieval.
        /// </summary>
        [HttpGet("EventTask")]
        public async Task<ActionResult<IEnumerable<EventTask>>> GetEventTask()
        {
            IEnumerable<EventTask> eventTask = await _eventTaskService.GetEventTasks();
            return Ok(eventTask);
        }

        /// <summary>
        /// Retrieves an event task by its unique identifier.
        /// If the task exists, it is returned with an HTTP 200 status code.
        /// If no task is found with the provided ID, an HTTP 404 status code is returned.
        /// </summary>
        [HttpGet("GetEventTaskById")]
        public async Task<ActionResult<EventTask>> FindEventTask(int id)
        {
            var eventTask = await _eventTaskService.GetEventTaskById(id);
            if (eventTask == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(eventTask);
            }
        }

        /// <summary>
        /// Retrieves an event task based on its name.
        /// The method searches for a task with the exact name provided.
        /// If found, the task details are returned; otherwise, a 404 Not Found response is sent.
        /// </summary>
        [HttpGet("GetEventTaskByName")]
        public async Task<ActionResult<EventTask>> FindEventTaskByName(string name)
        {
            var eventTask = await _eventTaskService.GetEventTaskByName(name);
            if (eventTask == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(eventTask);
            }
        }

        /// <summary>
        /// Retrieves all event tasks belonging to a specific category.
        /// The category is provided as an input parameter.
        /// If no tasks are found under this category, a 404 response is returned.
        /// </summary>
        [HttpGet("GetEventTasksByCategory")]
        public async Task<ActionResult<List<EventTask>>> FindEventTasksByCategory(EventTaskCategory category)
        {
            var eventTasks = await _eventTaskService.GetEventTasksByCategory(category);
            if (eventTasks == null || !eventTasks.Any())
            {
                return NotFound();
            }
            else
            {
                return Ok(eventTasks);
            }
        }

        /// <summary>
        /// Retrieves event tasks based on their completion status.
        /// If completed is true, it fetches all completed tasks.
        /// If completed is false, it fetches all pending tasks.
        /// </summary>
        [HttpGet("GetEventTasksByCompleted")]
        public async Task<ActionResult<List<EventTask>>> FindEventTasksByCompleted(bool completed)
        {
            var eventTasks = await _eventTaskService.GetEventTasksByCompleted(completed);
            if (eventTasks == null || !eventTasks.Any())
            {
                return NotFound();
            }
            else
            {
                return Ok(eventTasks);
            }
        }
        [HttpGet("GetEventTasksByAdminId/{id}")]
        public async Task<ActionResult<List<EventTask>>> GetEventTasksByAdminId(int id)
        {
            var eventTasks = await _eventTaskService.GetEventTasksByAdminId(id);
            if (eventTasks == null || !eventTasks.Any())
            {
                return NotFound();
            }
            else
            {
                return Ok(eventTasks);
            }
        }

        /// <summary>
        /// Adds a new event task to the system.
        /// The task details are received in the request body.
        /// Returns a 201 Created response upon successful addition.
        /// If there is an error, an appropriate error response is returned.
        /// </summary>
        [HttpPost("AddEventTask")]
        public async Task<ActionResult<EventTaskDto>> AddEventTask(EventTaskDto addeventtask)
        {
            ServiceResponse response = await _eventTaskService.AddEventTask(addeventtask);
            if (response.Status == ServiceResponse.ServiceStatus.NotFound)
            {
                return NotFound(response.Messages);
            }
            else if (response.Status == ServiceResponse.ServiceStatus.Error)
            {
                return StatusCode(500, response.Messages);
            }
            return Created($"api/EventTask/GetEventTaskById/{response.CreatedId}", addeventtask);
        }

        /// <summary>
        /// Updates an existing event task.
        /// The task ID must match the provided update details.
        /// If the update is successful, an HTTP 204 No Content response is returned.
        /// </summary>
        [HttpPut("UpdateEventTask/{id}")]
        public async Task<ActionResult<EventTask>> UpdateEventTask(int id, EventTask updateeventtask)
        {
            if (id != updateeventtask.TaskId)
            {
                return BadRequest();
            }
            ServiceResponse response = await _eventTaskService.UpdateEventTask(updateeventtask);
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
        /// Deletes an event task from the system.
        /// The task is identified using its unique ID.
        /// If the task is successfully deleted, a success message is returned.
        /// </summary>
        [HttpDelete("DeleteEventTask/{id}")]
        public async Task<ActionResult<EventTask>> DeleteEventTask(int id)
        {
            ServiceResponse response = await _eventTaskService.DeleteEventTask(id);
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
