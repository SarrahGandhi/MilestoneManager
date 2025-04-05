using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Milestonemanager.Data;
using Milestonemanager.Interfaces;
using Milestonemanager.Models;
namespace Milestonemanager.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PlaylistSongAPIController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public PlaylistSongAPIController(ApplicationDbContext context)
        {
            _context = context;

        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PlaylistSong>>> GetPlaylistSongs()
        {
            var playlistSongs = await _context.PlaylistSongs
                .Select(s => new
                {
                    s.PlaylistSongID,
                    s.PlaylistID,
                    PlaylistName = s.Playlist.Name,  // Fetch Playlist Name
                    s.SongID,
                    SongTitle = s.Song.Title,  // Fetch Song Title
                    s.Order
                })
                .ToListAsync();

            return Ok(playlistSongs);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetPlaylistSong(int id)
        {
            var playlistSong = await _context.PlaylistSongs
                .Where(ps => ps.PlaylistSongID == id)
                .Select(ps => new
                {
                    ps.PlaylistSongID,
                    ps.PlaylistID,
                    PlaylistName = ps.Playlist.Name,  // Fetching Playlist Name
                    ps.SongID,
                    SongTitle = ps.Song.Title,  // Fetching Song Title
                    ps.Order
                })
                .FirstOrDefaultAsync();

            if (playlistSong == null)
            {
                return NotFound();
            }

            return Ok(playlistSong);
        }

        [HttpPost]
        public async Task<ActionResult<PlaylistSong>> CreatePlaylistSong(PlaylistSong playlistSong)
        {
            var newPlaylistSong = new PlaylistSong()
            {
                PlaylistID = playlistSong.PlaylistID,
                SongID = playlistSong.SongID,
                Order = playlistSong.Order,
            };
            _context.PlaylistSongs.Add(newPlaylistSong);
            await _context.SaveChangesAsync();
            return Ok(newPlaylistSong);
        }
        [HttpPut("{id}")]
        public async Task<ActionResult<PlaylistSong>> UpdatePlaylistSong(int id, PlaylistSong playlistSong)
        {
            var playlistSongToUpdate = await _context.PlaylistSongs.FindAsync(id);
            if (playlistSongToUpdate == null)
            {
                return NotFound();
            }
            playlistSongToUpdate.PlaylistID = playlistSong.PlaylistID;
            playlistSongToUpdate.SongID = playlistSong.SongID;
            playlistSongToUpdate.Order = playlistSong.Order;
            await _context.SaveChangesAsync();
            return Ok(playlistSongToUpdate);
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult<PlaylistSong>> DeletePlaylistSong(int id)
        {
            var playlistSong = await _context.PlaylistSongs.FindAsync(id);
            if (playlistSong == null)
            {
                return NotFound();
            }
            _context.PlaylistSongs.Remove(playlistSong);
            await _context.SaveChangesAsync();
            return Ok(playlistSong);
        }
    }

}