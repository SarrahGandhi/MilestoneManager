using Milestonemanager.Models;
using MilestoneManager.Models;

namespace MilestoneManager.Interfaces
{
    public interface IEventTaskService
    {
        Task<IEnumerable<EventTask>> GetEventTasks();
        Task<EventTaskDto> GetEventTaskById(int id);
        Task<EventTaskDto> GetEventTaskByName(string name);
        Task<List<EventTask>> GetEventTasksByCategory(EventTaskCategory category);
        Task<List<EventTask>> GetEventTasksByCompleted(bool completed);
        Task<List<EventTask>> GetEventTasksByEvent(int eventId);
        Task<List<EventTask>> GetEventTasksByAdminId(int adminId);
        Task<List<EventTask>> GetEventTasksByDate(DateTime date);
        Task<ServiceResponse> AddEventTask(EventTaskDto eventTaskDto);
        Task<ServiceResponse> UpdateEventTask(EventTask eventTask);
        Task<ServiceResponse> DeleteEventTask(int id);
    }
}
