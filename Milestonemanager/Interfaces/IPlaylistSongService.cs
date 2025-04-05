using Milestonemanager.Models;
using MilestoneManager.Models;

namespace MilestoneManager.Interfaces
{
    public interface IPlaylistSongService
    {
        Task<IEnumerator<PlaylistSong>> GetAllPlaylistSongs();
        Task<PlaylistSong> GetPlaylistSong(int id);
        Task<IEnumerator<PlaylistSong>> GetPlaylistSongsByPlaylist(int playlistId);
        Task<ServiceResponse> CreatePlaylistSong(PlaylistSongDTO playlistSongDTO);
        Task<ServiceResponse> UpdatePlaylistSong(PlaylistSongDTO playlistSongDTO);
        Task<ServiceResponse> DeletePlaylistSong(int id);
    }
}