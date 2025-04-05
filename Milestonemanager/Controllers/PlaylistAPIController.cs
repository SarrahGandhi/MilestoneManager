using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Milestonemanager.Data;
using Milestonemanager.Interfaces;
using Milestonemanager.Models;
namespace Milestonemanager.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PlaylistAPIController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public PlaylistAPIController(ApplicationDbContext context)
        {
            _context = context;

        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Playlist>>> GetPlaylists()
        {
            var playlists = await _context.Playlists.Select(s => new PlaylistDTO
            {
                PlaylistID = s.PlaylistID,
                Name = s.Name,
                CreatedBy = s.CreatedBy,
            }).ToListAsync();
            return Ok(playlists);

        }
        [HttpGet("{id}")]
        public async Task<ActionResult<Playlist>> GetPlaylist(int id)
        {
            var playlist = await _context.Playlists.FindAsync(id);
            if (playlist == null)
            {
                return NotFound();
            }
            var playlistDto = new PlaylistDTO()
            {
                PlaylistID = playlist.PlaylistID,
                Name = playlist.Name,
                CreatedBy = playlist.CreatedBy,
            };
            return Ok(playlistDto);
        }
        [HttpPost]
        public async Task<ActionResult<Playlist>> CreatePlaylist(PlaylistDTO playlist)
        {
            var newPlaylist = new Playlist()
            {
                Name = playlist.Name,
                CreatedBy = playlist.CreatedBy,
            };
            _context.Playlists.Add(newPlaylist);
            await _context.SaveChangesAsync();
            return Ok(newPlaylist);
        }
        [HttpPut("{id}")]
        public async Task<ActionResult<Playlist>> UpdatePlaylist(int id, PlaylistDTO playlist)
        {
            var playlistToUpdate = await _context.Playlists.FindAsync(id);
            if (playlistToUpdate == null)
            {
                return NotFound();
            }
            playlistToUpdate.Name = playlist.Name;
            playlistToUpdate.CreatedBy = playlist.CreatedBy;
            await _context.SaveChangesAsync();
            return Ok(playlistToUpdate);
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult<Playlist>> DeletePlaylist(int id)
        {
            var playlistToDelete = await _context.Playlists.FindAsync(id);
            if (playlistToDelete == null)
            {
                return NotFound();
            }
            _context.Playlists.Remove(playlistToDelete);
            await _context.SaveChangesAsync();
            return Ok(playlistToDelete);
        }

    }
}