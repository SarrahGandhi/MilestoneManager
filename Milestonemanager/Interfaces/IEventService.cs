using Milestonemanager.Models;
using MilestoneManager.Models;

namespace MilestoneManager.Interfaces
{
    public interface IEventService
    {
        Task<IEnumerable<Event>> GetEvents();
        Task<EventDto> GetEventById(int id);
        Task<EventDto> GetEventByName(string name);
        Task<List<Event>> GetEventsByCategory(EventCategory category);
        Task<List<Event>> GetEventsByDate(DateTime date);
        Task<List<Event>> GetEventsByLocation(string location);
        Task<ServiceResponse> AddEvent(EventDto eventDto);
        Task<ServiceResponse> UpdateEvent(Event event1);
        Task<ServiceResponse> DeleteEvent(int id);

    }
}