using Milestonemanager.Models;
using MilestoneManager.Models;
namespace MilestoneManager.Interfaces
{
    public interface IEventGuestService
    {
        Task<IEnumerable<EventGuest>> GetEventGuests();
        Task<EventGuest> GetEventGuestById(int id);
        Task<EventGuestDto> GetEventGuestByEvent(int eventId);
        Task<EventGuestDto> GetEventGuestByGuest(int guestId);
        Task<List<EventGuest>> GetEventGuestsByIsRSVPAccepted(bool isRSVPAccepted);
        Task<List<EventGuest>> GetEventGuestsByEventMen(int eventMen);
        Task<List<EventGuest>> GetEventGuestsByEventWomen(int eventWomen);
        Task<List<EventGuest>> GetEventGuestsByEventKids(int eventKids);
        Task<ServiceResponse> AddEventGuest(EventGuestDto eventGuestDto);
        Task<ServiceResponse> UpdateEventGuest(EventGuestDto eventGuest);
        Task<ServiceResponse> DeleteEventGuest(int id);
    }
}