

using Milestonemanager.Models;
using MilestoneManager.Models;

namespace Milestonemanager.Interfaces
{
    public interface IAdminService
    {
        Task<IEnumerable<Admin>> GetAdmins();
        Task<Admin> GetAdminById(int id);

        Task<ServiceResponse> AddAdmin(AdminDto adminDto);
        Task<ServiceResponse> UpdateAdmin(AdminDto adminDto);
        Task<ServiceResponse> DeleteAdmin(int id);
    }
}