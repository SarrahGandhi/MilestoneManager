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
        [HttpGet]
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
        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {

            var tasks = await _taskService.GetEventTasksByAdminId(id);
            var admin = await _adminService.GetAdminById(id);
            ViewData["Tasks"] = tasks;
            ViewData["AdminName"] = admin.AdminName;
            return View();
        }

        // Show the form to create a new admin
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        // Handle the creation of a new admin
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateAdmin(AdminDto adminDto)
        {

            var response = await _adminService.AddAdmin(adminDto);
            if (response.Status == ServiceResponse.ServiceStatus.Created)
            {
                return RedirectToAction("ListAdmin", "AdminPage");
            }
            else
            {
                return RedirectToAction("Create", "AdminPage");
            }



        }

        // Show the edit form for an admin
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var admin = await _adminService.GetAdminById(id);
            if (admin == null)
            {
                return NotFound();
            }

            // Convert Admin to AdminDto
            var adminDto = new AdminDto
            {
                AdminId = id,
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
        public async Task<IActionResult> EditAdmin(AdminDto adminDto)
        {

            var response = await _adminService.UpdateAdmin(adminDto);

            if (response.Status == ServiceResponse.ServiceStatus.Updated)
            {
                return RedirectToAction("ListAdmin", "AdminPage");
            }
            else
            {
                return RedirectToAction("Edit", "AdminPage");
            }

        }

        // Show the delete confirmation page
        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var admin = await _adminService.GetAdminById(id);
            var adminDto = new AdminDto
            {
                AdminId = id,
                AdminName = admin.AdminName,
                AdminEmail = admin.AdminEmail,
                AdminPhone = admin.AdminPhone,
                AdminCategory = admin.AdminCategory
            };
            return View(adminDto);
        }

        // Handle the deletion of an admin
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteAdmin(int id)
        {
            var response = await _adminService.DeleteAdmin(id);
            if (response.Status == ServiceResponse.ServiceStatus.Deleted)
            {
                return RedirectToAction("ListAdmin", "AdminPage");
            }
            else
            {
                return RedirectToAction("Delete", "AdminPage");
            }

        }
    }
}
