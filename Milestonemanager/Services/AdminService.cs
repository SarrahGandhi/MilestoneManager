using Milestonemanager.Data;
using Milestonemanager.Interfaces;
using System;
using MilestoneManager.Models;
using Milestonemanager.Models;
using Microsoft.EntityFrameworkCore;

namespace CoreEntityFramework.Services
{
    public class AdminService : IAdminService
    {
        private readonly ApplicationDbContext _context;
        public AdminService(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Admin>> GetAdmins()
        {
            List<Admin> admins = await _context.Admins.ToListAsync();
            List<Admin> adminlist = new List<Admin>();
            foreach (var admin in admins)
            {
                adminlist.Add(new Admin()
                {
                    AdminId = admin.AdminId,
                    AdminName = admin.AdminName,
                    AdminEmail = admin.AdminEmail,
                    AdminPhone = admin.AdminPhone,
                    AdminCategory = admin.AdminCategory
                });
            }
            return adminlist;
        }

        public async Task<Admin> GetAdminById(int id)
        {
            var admin = await _context.Admins.FindAsync(id);
            if (admin == null)
            {
                return null;
            }
            Admin admins = new Admin()
            {
                AdminId = admin.AdminId,
                AdminName = admin.AdminName,
                AdminEmail = admin.AdminEmail,
                AdminPhone = admin.AdminPhone,
                AdminCategory = admin.AdminCategory
            };
            return admins;
        }
        public async Task<ServiceResponse> AddAdmin(AdminDto adminDto)
        {
            ServiceResponse serviceResponse = new ServiceResponse();
            Admin admin = new Admin()
            {
                AdminName = adminDto.AdminName,
                AdminEmail = adminDto.AdminEmail,
                AdminPhone = adminDto.AdminPhone,
                AdminCategory = adminDto.AdminCategory
            };
            try
            {
                _context.Admins.Add(admin);
                await _context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                serviceResponse.Status = ServiceResponse.ServiceStatus.Error;
                serviceResponse.Messages.Add(e.Message);
            }
            serviceResponse.Status = ServiceResponse.ServiceStatus.Created;
            serviceResponse.CreatedId = admin.AdminId;
            return serviceResponse;
        }
        public async Task<ServiceResponse> UpdateAdmin(AdminDto adminDto)
        {
            ServiceResponse serviceResponse = new ServiceResponse();
            Admin addadmin = new Admin()
            {
                AdminId = adminDto.AdminId,
                AdminName = adminDto.AdminName,
                AdminEmail = adminDto.AdminEmail,
                AdminPhone = adminDto.AdminPhone,
                AdminCategory = adminDto.AdminCategory
            };
            _context.Entry(addadmin).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                serviceResponse.Status = ServiceResponse.ServiceStatus.Error;
                serviceResponse.Messages.Add("An Error Occured");
                return serviceResponse;
            }
            serviceResponse.Status = ServiceResponse.ServiceStatus.Updated;
            return serviceResponse;

        }
        public async Task<ServiceResponse> DeleteAdmin(int id)
        {
            ServiceResponse response = new ServiceResponse();
            var admin = await _context.Admins.FindAsync(id);
            if (admin == null)
            {
                response.Status = ServiceResponse.ServiceStatus.NotFound;
                response.Messages.Add("Admin Not Found");
                return response;
            }
            try
            {
                _context.Admins.Remove(admin);
                await _context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                response.Status = ServiceResponse.ServiceStatus.Error;
                response.Messages.Add(e.Message);
                return response;
            }
            response.Status = ServiceResponse.ServiceStatus.Deleted;
            return response;
        }





    }
}