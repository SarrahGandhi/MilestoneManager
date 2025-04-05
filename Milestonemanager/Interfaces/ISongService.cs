using Milestonemanager.Models;
using MilestoneManager.Models;
namespace Milestonemanager.Interfaces
{
    public interface ISongService
    {
        Task<IEnumerable<Song>> GetAllSongs();
        Task<Song> GetSong(int id);
        Task<ServiceResponse> CreateSong(SongDTO songDTO);
        Task<ServiceResponse> UpdateSong(SongDTO songDTO);
        Task<ServiceResponse> DeleteSong(int id);
    }
}