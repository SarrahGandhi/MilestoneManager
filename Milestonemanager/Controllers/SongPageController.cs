using Microsoft.AspNetCore.Mvc;
using Milestonemanager.Interfaces;
using Milestonemanager.Models;
using MilestoneManager.Interfaces;
using MilestoneManager.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MilestoneManager.Controllers
{
    public class SongPageController : Controller
    {
        private readonly ISongService _songService;

        public SongPageController(ISongService songService)
        {
            _songService = songService;

        }

        public IActionResult Index()
        {
            return RedirectToAction("ListSong");
        }
        public async Task<IActionResult> ListSong()
        {
            var songs = await _songService.GetAllSongs();
            var songDtos = songs.Select(songItem => new SongDTO
            {
                SongID = songItem.SongID,
                Title = songItem.Title,
                Artist = songItem.Artist,
                Genre = songItem.Genre,
                Description = songItem.Description
            });
            return View(songDtos);
        }
        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var song = await _songService.GetSong(id);
            ViewData["SongName"] = song.Title;
            return View();

        }
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken()]
        public async Task<IActionResult> CreateSong(SongDTO songDto)
        {
            var response = await _songService.CreateSong(songDto);
            if (response.Status == ServiceResponse.ServiceStatus.Created)
            {
                return RedirectToAction("ListSong", "SongPage");
            }
            else
            {
                return RedirectToAction("Create", "SongPage");
            }
        }
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var song = await _songService.GetSong(id);
            if (song == null)
            {
                return NotFound();
            }
            var songDto = new SongDTO
            {
                SongID = id,
                Title = song.Title,
                Artist = song.Artist,
                Genre = song.Genre,
                Description = song.Description
            };
            return View(songDto);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditSong(SongDTO songDto)
        {
            var response = await _songService.UpdateSong(songDto);
            if (response.Status == ServiceResponse.ServiceStatus.Updated)
            {
                return RedirectToAction("ListSong", "SongPage");
            }
            else
            {
                return RedirectToAction("Edit", "SongPage");
            }
        }
        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var song = await _songService.GetSong(id);
            var songDto = new SongDTO
            {
                SongID = id,
                Title = song.Title,
                Artist = song.Artist,
                Genre = song.Genre,
                Description = song.Description
            };
            return View(songDto);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteSong(int id)
        {
            var response = await _songService.DeleteSong(id);
            if (response.Status == ServiceResponse.ServiceStatus.Deleted)
            {
                return RedirectToAction("ListSong", "SongPage");
            }
            else
            {
                return RedirectToAction("Delete", "SongPage");
            }
        }


    }
}
