using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Milestonemanager.Data;
using Milestonemanager.Interfaces;
using Milestonemanager.Models;
using MilestoneManager.Interfaces;
using MilestoneManager.Models;
namespace Milestonemanager.Controllers

{
    [ApiController]
    [Route("[controller]")]
    public class SongAPIController : ControllerBase
    {
        private readonly ISongService _songService;
        public SongAPIController(ISongService songService)
        {
            _songService = songService;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Song>>> GetSongs()
        {
            var songs = await _songService.GetAllSongs();
            return Ok(songs);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetSong(int id)
        {
            var song = await _songService.GetSong(id);
            if (song == null)
            {
                return NotFound();
            }
            return Ok(song);
        }
        [HttpPost]
        public async Task<ActionResult<Song>> CreateSong(SongDTO song)
        {
            ServiceResponse response = await _songService.CreateSong(song);
            if (response.Status == ServiceResponse.ServiceStatus.NotFound)
            {
                return NotFound(response.Messages);
            }
            else if (response.Status == ServiceResponse.ServiceStatus.Error)
            {
                return StatusCode(500, response.Messages);
            }
            return Created($"api/Song/GetSong/{response.CreatedId}", song);
        }
        [HttpPut("{id}")]
        public async Task<ActionResult<Song>> UpdateSong(int id, SongDTO song)
        {
            if (id != song.SongID)
            {
                return BadRequest();
            }
            ServiceResponse response = await _songService.UpdateSong(song);
            if (response.Status == ServiceResponse.ServiceStatus.NotFound)
            {
                return NotFound(response.Messages);
            }
            else if (response.Status == ServiceResponse.ServiceStatus.Error)
            {
                return StatusCode(500, response.Messages);
            }
            return NoContent();
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult<Song>> DeleteSong(int id)
        {
            ServiceResponse response = await _songService.DeleteSong(id);
            if (response.Status == ServiceResponse.ServiceStatus.NotFound)
            {
                return NotFound(response.Messages);
            }
            else if (response.Status == ServiceResponse.ServiceStatus.Error)
            {
                return StatusCode(500, response.Messages);
            }
            return Ok(response.Messages);
        }
    }
}