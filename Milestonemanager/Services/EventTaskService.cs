using Microsoft.EntityFrameworkCore;
using Milestonemanager.Data;
using Milestonemanager.Models;
using MilestoneManager.Interfaces;
using MilestoneManager.Models;

namespace CoreEntityFramework.Services
{
    public class EventTaskService : IEventTaskService
    {
        private readonly ApplicationDbContext _context;
        public EventTaskService(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<EventTask>> GetEventTasks()
        {
            List<EventTask> eventTasks = await _context.EventTasks.ToListAsync();
            List<EventTask> eventTaskList = new List<EventTask>();
            foreach (var eventTask in eventTasks)
            {
                eventTaskList.Add(new EventTask()
                {
                    TaskId = eventTask.TaskId,
                    TaskDescription = eventTask.TaskDescription,
                    DueDate = eventTask.DueDate,
                    EventId = eventTask.EventId,
                    AdminId = eventTask.AdminId,
                    IsCompleted = eventTask.IsCompleted,
                    EventTaskCategory = eventTask.EventTaskCategory
                });
            }
            return eventTaskList;
        }
        public async Task<EventTaskDto> GetEventTaskById(int id)
        {
            var eventTask = await _context.EventTasks.FindAsync(id);
            if (eventTask == null)
            {
                return null;
            }
            EventTaskDto eventTasks = new EventTaskDto()
            {
                TaskDescription = eventTask.TaskDescription,
                DueDate = eventTask.DueDate,
                EventId = eventTask.EventId,
                AdminId = eventTask.AdminId,
                IsCompleted = eventTask.IsCompleted,
                EventTaskCategory = eventTask.EventTaskCategory
            };
            return eventTasks;
        }
        public async Task<EventTaskDto> GetEventTaskByName(string name)
        {
            var eventTask = await _context.EventTasks.FirstOrDefaultAsync(x => x.TaskDescription == name);
            if (eventTask == null)
            {
                return null;
            }
            EventTaskDto eventTasks = new EventTaskDto()
            {
                TaskDescription = eventTask.TaskDescription,
                DueDate = eventTask.DueDate,
                EventId = eventTask.EventId,
                AdminId = eventTask.AdminId,
                IsCompleted = eventTask.IsCompleted,
                EventTaskCategory = eventTask.EventTaskCategory
            };
            return eventTasks;
        }
        public async Task<List<EventTask>> GetEventTasksByCategory(EventTaskCategory category)
        {
            return await _context.EventTasks
                                 .Where(e => e.EventTaskCategory == category)
                                 .ToListAsync();
        }
        public async Task<List<EventTask>> GetEventTasksByCompleted(bool completed)
        {
            return await _context.EventTasks
                                 .Where(e => e.IsCompleted == completed)
                                 .ToListAsync();
        }
        public async Task<List<EventTask>> GetEventTasksByEvent(int eventId)
        {
            return await _context.EventTasks
                                 .Where(e => e.EventId == eventId)
                                 .ToListAsync();
        }
        public async Task<List<EventTask>> GetEventTasksByAdminId(int adminId)
        {
            return await _context.EventTasks.Where(e => e.AdminId == adminId).ToListAsync();
        }

        public async Task<List<EventTask>> GetEventTasksByDate(DateTime date)
        {
            return await _context.EventTasks
                                 .Where(e => e.DueDate == date)
                                 .ToListAsync();
        }
        public async Task<ServiceResponse> AddEventTask(EventTaskDto eventTaskDto)
        {
            ServiceResponse serviceResponse = new ServiceResponse();
            EventTask eventTask = new EventTask()
            {
                TaskDescription = eventTaskDto.TaskDescription,
                DueDate = eventTaskDto.DueDate,
                EventId = eventTaskDto.EventId,
                AdminId = eventTaskDto.AdminId,
                IsCompleted = eventTaskDto.IsCompleted,
                EventTaskCategory = eventTaskDto.EventTaskCategory
            };
            try
            {
                _context.EventTasks.Add(eventTask);
                await _context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                serviceResponse.Status = ServiceResponse.ServiceStatus.Error;
                serviceResponse.Messages.Add(e.Message);
            }
            serviceResponse.Status = ServiceResponse.ServiceStatus.Created;
            serviceResponse.CreatedId = eventTask.TaskId;
            return serviceResponse;
        }
        public async Task<ServiceResponse> UpdateEventTask(EventTask eventTask)
        {
            ServiceResponse serviceResponse = new ServiceResponse();
            EventTask addEventTask = new EventTask()
            {
                TaskDescription = eventTask.TaskDescription,
                DueDate = eventTask.DueDate,
                EventId = eventTask.EventId,
                AdminId = eventTask.AdminId,
                IsCompleted = eventTask.IsCompleted,
                EventTaskCategory = eventTask.EventTaskCategory
            };
            _context.Entry(addEventTask).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                serviceResponse.Status = ServiceResponse.ServiceStatus.Error;
                serviceResponse.Messages.Add("EventTask Already Exists");
                return serviceResponse;
            }
            serviceResponse.Status = ServiceResponse.ServiceStatus.Updated;
            return serviceResponse;
        }
        public async Task<ServiceResponse> DeleteEventTask(int id)
        {
            ServiceResponse response = new ServiceResponse();
            var eventTask = await _context.EventTasks.FindAsync(id);
            if (eventTask == null)
            {
                response.Status = ServiceResponse.ServiceStatus.NotFound;
                response.Messages.Add("EventTask Not Found");
                return response;
            }
            try
            {
                _context.EventTasks.Remove(eventTask);
                await _context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                response.Status = ServiceResponse.ServiceStatus.Error;
                response.Messages.Add(e.Message);
                return response;
            }
            response.Status = ServiceResponse.ServiceStatus.Deleted;
            return response;
        }
    }
}