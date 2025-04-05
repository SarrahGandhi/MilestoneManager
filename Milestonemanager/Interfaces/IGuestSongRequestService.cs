using Milestonemanager.Models;
using MilestoneManager.Models;

namespace MilestoneManager.Interfaces
{
    public interface IGuestSongRequestService
    {
        Task<IEnumerator<GuestSongRequest>> GetAllGuestSongRequests();
        Task<GuestSongRequest> GetGuestSongRequest(int id);
        Task<IEnumerator<GuestSongRequest>> GetGuestSongRequestsByGuest(int guestId);
        Task<ServiceResponse> CreateGuestSongRequest(GuestSongRequestDTO guestSongRequestDTO);
        Task<ServiceResponse> UpdateGuestSongRequest(GuestSongRequestDTO guestSongRequestDTO);
        Task<ServiceResponse> DeleteGuestSongRequest(int id);
    }
}