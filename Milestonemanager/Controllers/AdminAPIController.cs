using Microsoft.AspNetCore.Mvc;
using Milestonemanager.Interfaces;
using Milestonemanager.Models;
using MilestoneManager.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Milestonemanager.Controllers
{
    /// <summary>
    /// API Controller for managing Admin entities.
    /// Provides endpoints to retrieve, add, update, and delete admins.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class AdminAPIController : ControllerBase
    {
        private readonly IAdminService _adminService;

        /// <summary>
        /// Initializes a new instance of the <see cref="AdminAPIController"/> class.
        /// </summary>
        /// <param name="adminService">The service handling admin-related operations.</param>
        public AdminAPIController(IAdminService adminService)
        {
            _adminService = adminService;
        }

        /// <summary>
        /// Retrieves all admin records.
        /// </summary>
        /// <returns>A list of admin objects.</returns>
        [HttpGet("Admin")]
        public async Task<ActionResult<IEnumerable<Admin>>> GetAdmins()
        {
            IEnumerable<Admin> admin = await _adminService.GetAdmins();
            return Ok(admin);
        }

        /// <summary>
        /// Retrieves an admin by their unique ID.
        /// </summary>
        /// <param name="id">The ID of the admin.</param>
        /// <returns>The requested admin object, or NotFound if not found.</returns>
        [HttpGet("GetAdminById")]
        public async Task<ActionResult<Admin>> FindAdmin(int id)
        {
            var admin = await _adminService.GetAdminById(id);
            if (admin == null)
            {
                return NotFound();
            }
            return Ok(admin);
        }

        /// <summary>
        /// Adds a new admin to the system.
        /// </summary>
        /// <param name="addadmin">The admin data transfer object.</param>
        /// <returns>The created admin object with a response status.</returns>
        [HttpPost("AddAdmin")]
        public async Task<ActionResult<AdminDto>> AddAdmin(AdminDto addadmin)
        {
            ServiceResponse response = await _adminService.AddAdmin(addadmin);
            if (response.Status == ServiceResponse.ServiceStatus.NotFound)
            {
                return NotFound(response.Messages);
            }
            else if (response.Status == ServiceResponse.ServiceStatus.Error)
            {
                return StatusCode(500, response.Messages);
            }
            return Created($"api/Admin/GetAdminById/{response.CreatedId}", addadmin);
        }

        /// <summary>
        /// Updates an existing admin record.
        /// </summary>
        /// <param name="id">The ID of the admin to update.</param>
        /// <param name="updateadmin">The updated admin object.</param>
        /// <returns>NoContent if successful, or an appropriate error response.</returns>
        [HttpPut("UpdateAdmin/{id}")]
        public async Task<ActionResult> UpdateAdmin(int id, Admin updateadmin)
        {
            if (id != updateadmin.AdminId)
            {
                return BadRequest();
            }
            ServiceResponse response = await _adminService.UpdateAdmin(updateadmin);
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

        /// <summary>
        /// Deletes an admin record by ID.
        /// </summary>
        /// <param name="id">The ID of the admin to delete.</param>
        /// <returns>A success message or an appropriate error response.</returns>
        [HttpDelete("DeleteAdmin/{id}")]
        public async Task<ActionResult<Admin>> DeleteAdmin(int id)
        {
            ServiceResponse response = await _adminService.DeleteAdmin(id);
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
