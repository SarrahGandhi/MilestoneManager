using Microsoft.EntityFrameworkCore;
using Milestonemanager.Data;
using Milestonemanager.Models;
using MilestoneManager.Interfaces;
using MilestoneManager.Models;

namespace CoreEntityFramework.Services
{
    public class GuestService : IGuestService
    {

        private readonly ApplicationDbContext _context;
        public GuestService(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Guest>> GetGuests()
        {
            List<Guest> guests = await _context.Guests.ToListAsync();
            List<Guest> guestlist = new List<Guest>();
            foreach (var guest in guests)
            {
                guestlist.Add(new Guest()
                {
                    GuestId = guest.GuestId,
                    GuestName = guest.GuestName,
                    GuestLocation = guest.GuestLocation,
                    GuestPhone = guest.GuestPhone,
                    IsInvited = guest.IsInvited,
                    GuestCategory = guest.GuestCategory
                });
            }
            return guestlist;
        }
        public async Task<GuestDto> GetGuestById(int id)
        {
            var guest = await _context.Guests.FindAsync(id);
            if (guest == null)
            {
                return null;
            }
            GuestDto guests = new GuestDto()
            {
                GuestName = guest.GuestName,
                GuestLocation = guest.GuestLocation,
                GuestPhone = guest.GuestPhone,
                IsInvited = guest.IsInvited,
                GuestCategory = guest.GuestCategory
            };
            return guests;
        }
        public async Task<GuestDto> GetGuestByName(string name)
        {
            var guest = await _context.Guests.FirstOrDefaultAsync(x => x.GuestName == name);
            if (guest == null)
            {
                return null;
            }
            GuestDto guests = new GuestDto()
            {
                GuestName = guest.GuestName,
                GuestLocation = guest.GuestLocation,
                GuestPhone = guest.GuestPhone,
                IsInvited = guest.IsInvited,
                GuestCategory = guest.GuestCategory
            };
            return guests;
        }
        public async Task<List<Guest>> GetGuestsByCategory(GuestCategory category)
        {
            return await _context.Guests
                                 .Where(g => g.GuestCategory == category)
                                 .ToListAsync();
        }


        public async Task<ServiceResponse> AddGuest(GuestDto guestDto)
        {
            ServiceResponse serviceResponse = new ServiceResponse();
            Guest guest = new Guest()
            {
                GuestName = guestDto.GuestName,
                GuestLocation = guestDto.GuestLocation,
                GuestPhone = guestDto.GuestPhone,
                IsInvited = guestDto.IsInvited,
                GuestCategory = guestDto.GuestCategory
            };
            try
            {
                _context.Guests.Add(guest);
                await _context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                serviceResponse.Status = ServiceResponse.ServiceStatus.Error;
                serviceResponse.Messages.Add(e.Message);
            }
            serviceResponse.Status = ServiceResponse.ServiceStatus.Created;
            serviceResponse.CreatedId = guest.GuestId;
            return serviceResponse;
        }
        public async Task<ServiceResponse> UpdateGuest(Guest guest)
        {
            ServiceResponse serviceResponse = new ServiceResponse();
            Guest addguest = new Guest()
            {
                GuestName = guest.GuestName,
                GuestLocation = guest.GuestLocation,
                GuestPhone = guest.GuestPhone,
                IsInvited = guest.IsInvited,
                GuestCategory = guest.GuestCategory
            };
            _context.Entry(addguest).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                serviceResponse.Status = ServiceResponse.ServiceStatus.Error;
                serviceResponse.Messages.Add("Guest Already Exists");
                return serviceResponse;
            }
            serviceResponse.Status = ServiceResponse.ServiceStatus.Updated;
            return serviceResponse;
        }
        public async Task<ServiceResponse> DeleteGuest(int id)
        {
            ServiceResponse response = new ServiceResponse();
            var guest = await _context.Guests.FindAsync(id);
            if (guest == null)
            {
                response.Status = ServiceResponse.ServiceStatus.NotFound;
                response.Messages.Add("Guest Not Found");
                return response;
            }
            try
            {
                _context.Guests.Remove(guest);
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
