using Microsoft.AspNetCore.Mvc;
using Milestonemanager.Interfaces;
using Milestonemanager.Models;
using MilestoneManager.Interfaces;
using MilestoneManager.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Milestonemanager.Controllers
{
    public class AdminPageController : Controller
    {
        private readonly IAdminService _adminService;
        private readonly IEventTaskService _taskService;

        public AdminPageController(IAdminService adminService, IEventTaskService taskService)
        {
            _adminService = adminService;
            _taskService = taskService;
        }

        // List all admins
        public async Task<IActionResult> ListAdmin()
        {
            var admins = await _adminService.GetAdmins();
            var adminDtos = admins.Select(admin => new AdminDto
            {
                AdminId = admin.AdminId,
                AdminName = admin.AdminName,
                AdminEmail = admin.AdminEmail,
                AdminPhone = admin.AdminPhone,
                AdminCategory = admin.AdminCategory
            }).ToList();
            return View(adminDtos);
        }

        // View details of a single admin along with assigned tasks
        public async Task<IActionResult> DetailsAdmin(int id)
        {
            var admin = await _adminService.GetAdminById(id);
            if (admin == null)
            {
                return NotFound();
            }

            var tasks = await _taskService.GetEventTasksByAdminId(id);
            ViewBag.Tasks = tasks;
            return View(admin);
        }

        // Show the form to create a new admin
        public IActionResult CreateAdmin()
        {
            return View();
        }

        // Handle the creation of a new admin
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateAdmin(AdminDto adminDto)
        {
            if (ModelState.IsValid)
            {
                var response = await _adminService.AddAdmin(adminDto);
                if (response.Status == ServiceResponse.ServiceStatus.Success)
                {
                    return RedirectToAction(nameof(ListAdmin));
                }
                ModelState.AddModelError("", string.Join(", ", response.Messages));
            }
            return View(adminDto);
        }

        // Show the edit form for an admin
        public async Task<IActionResult> EditAdmin(int id)
        {
            var admin = await _adminService.GetAdminById(id);
            if (admin == null)
            {
                return NotFound();
            }

            // Convert Admin to AdminDto
            var adminDto = new AdminDto
            {
                AdminId = admin.AdminId,
                AdminName = admin.AdminName,
                AdminEmail = admin.AdminEmail,
                AdminPhone = admin.AdminPhone,
                AdminCategory = admin.AdminCategory
            };

            return View(adminDto);
        }


        // Handle the update of an admin
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditAdmin(int id, Admin admin)
        {
            if (id != admin.AdminId)
            {
                return BadRequest();
            }

            if (ModelState.IsValid)
            {
                var response = await _adminService.UpdateAdmin(admin);
                if (response.Status == ServiceResponse.ServiceStatus.Success)
                {
                    return RedirectToAction(nameof(ListAdmin));
                }
                ModelState.AddModelError("", string.Join(", ", response.Messages));
            }
            return View(admin);
        }

        // Show the delete confirmation page
        public async Task<IActionResult> DeleteAdmin(int id)
        {
            var admin = await _adminService.GetAdminById(id);
            if (admin == null) return NotFound();
            var adminDto = new AdminDto
            {
                AdminId = admin.AdminId,
                AdminName = admin.AdminName,
                AdminEmail = admin.AdminEmail,
                AdminPhone = admin.AdminPhone,
                AdminCategory = admin.AdminCategory
            };
            return View(adminDto);
        }

        // Handle the deletion of an admin
        [HttpPost, ActionName("DeleteAdmin")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var response = await _adminService.DeleteAdmin(id);
            if (response.Status == ServiceResponse.ServiceStatus.Success)
            {
                return RedirectToAction(nameof(ListAdmin));
            }
            ModelState.AddModelError("", string.Join(", ", response.Messages));
            return View();
        }
    }
}
