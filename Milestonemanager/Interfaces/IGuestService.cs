using Milestonemanager.Models;
using MilestoneManager.Models;

namespace MilestoneManager.Interfaces
{
    public interface IGuestService
    {
        Task<IEnumerable<Guest>> GetGuests();
        Task<GuestDto> GetGuestById(int id);
        Task<GuestDto> GetGuestByName(string name);
        Task<List<Guest>> GetGuestsByCategory(GuestCategory category);
        Task<ServiceResponse> AddGuest(GuestDto guestDto);
        Task<ServiceResponse> UpdateGuest(Guest guest);
        Task<ServiceResponse> DeleteGuest(int id);




    }
}