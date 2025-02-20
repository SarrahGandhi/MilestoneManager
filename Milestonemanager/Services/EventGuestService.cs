using Microsoft.EntityFrameworkCore;
using Milestonemanager.Data;
using Milestonemanager.Models;
using MilestoneManager.Interfaces;
using MilestoneManager.Models;
namespace CoreEntityFramework.Services
{
    public class EventGuestService : IEventGuestService
    {
        private readonly ApplicationDbContext _context;
        public EventGuestService(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<EventGuest>> GetEventGuests()
        {
            List<EventGuest> eventGuests = await _context.EventGuests.ToListAsync();
            List<EventGuest> eventGuestList = new List<EventGuest>();
            foreach (var eventGuest in eventGuests)
            {
                eventGuestList.Add(new EventGuest()
                {
                    GuestEventId = eventGuest.GuestEventId,
                    EventId = eventGuest.EventId,
                    EventMen = eventGuest.EventMen,
                    EventWomen = eventGuest.EventWomen,
                    EventKids = eventGuest.EventKids
                });
            }
            return eventGuestList;
        }
        public async Task<EventGuest> GetEventGuestById(int id)
        {
            var eventGuest = await _context.EventGuests.FindAsync(id);
            if (eventGuest == null)
            {
                return null;
            }
            EventGuest eventGuests = new EventGuest()
            {
                GuestEventId = eventGuest.GuestEventId,
                EventId = eventGuest.EventId,
                EventMen = eventGuest.EventMen,
                EventWomen = eventGuest.EventWomen,
                EventKids = eventGuest.EventKids
            };
            return eventGuests;
        }
        public async Task<EventGuestDto> GetEventGuestByEvent(int eventId)
        {
            var eventGuest = await _context.EventGuests.FirstOrDefaultAsync(x => x.EventId == eventId);
            if (eventGuest == null)
            {
                return null;
            }
            EventGuestDto eventGuests = new EventGuestDto()
            {
                EventId = eventGuest.EventId,
                GuestId = eventGuest.GuestId,
                IsRSVPAccepted = eventGuest.IsRSVPAccepted,
                EventMen = eventGuest.EventMen,
                EventWomen = eventGuest.EventWomen,
                EventKids = eventGuest.EventKids
            };
            return eventGuests;
        }
        public async Task<EventGuestDto> GetEventGuestByGuest(int guestId)
        {
            var eventGuest = await _context.EventGuests.FirstOrDefaultAsync(x => x.GuestId == guestId);
            if (eventGuest == null)
            {
                return null;
            }
            EventGuestDto eventGuests = new EventGuestDto()
            {
                EventId = eventGuest.EventId,
                GuestId = eventGuest.GuestId,
                IsRSVPAccepted = eventGuest.IsRSVPAccepted,
                EventMen = eventGuest.EventMen,
                EventWomen = eventGuest.EventWomen,
                EventKids = eventGuest.EventKids
            };
            return eventGuests;
        }
        public async Task<List<EventGuest>> GetEventGuestsByIsRSVPAccepted(bool isRSVPAccepted)
        {
            return await _context.EventGuests
                                 .Where(e => e.IsRSVPAccepted == isRSVPAccepted)
                                 .ToListAsync();
        }
        public async Task<List<EventGuest>> GetEventGuestsByEventMen(int eventMen)
        {
            return await _context.EventGuests
                                 .Where(e => e.EventMen == eventMen)
                                 .ToListAsync();
        }
        public async Task<List<EventGuest>> GetEventGuestsByEventWomen(int eventWomen)
        {
            return await _context.EventGuests
                                 .Where(e => e.EventWomen == eventWomen)
                                 .ToListAsync();
        }
        public async Task<List<EventGuest>> GetEventGuestsByEventKids(int eventKids)
        {
            return await _context.EventGuests
                                 .Where(e => e.EventKids == eventKids)
                                 .ToListAsync();
        }
        public async Task<ServiceResponse> AddEventGuest(EventGuestDto eventGuestDto)
        {
            ServiceResponse serviceResponse = new ServiceResponse();
            EventGuest eventGuest = new EventGuest()
            {
                GuestEventId = eventGuestDto.GuestId,
                EventId = eventGuestDto.EventId,
                EventMen = eventGuestDto.EventMen,
                EventWomen = eventGuestDto.EventWomen,
                EventKids = eventGuestDto.EventKids
            };
            try
            {
                _context.EventGuests.Add(eventGuest);
                await _context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                serviceResponse.Status = ServiceResponse.ServiceStatus.Error;
                serviceResponse.Messages.Add(e.Message);
            }
            serviceResponse.Status = ServiceResponse.ServiceStatus.Created;
            serviceResponse.CreatedId = eventGuest.GuestEventId;
            return serviceResponse;
        }
        public async Task<ServiceResponse> UpdateEventGuest(EventGuest eventGuest)
        {
            ServiceResponse serviceResponse = new ServiceResponse();
            EventGuest addEventGuest = new EventGuest()
            {
                GuestEventId = eventGuest.GuestEventId,
                EventId = eventGuest.EventId,
                EventMen = eventGuest.EventMen,
                EventWomen = eventGuest.EventWomen,
                EventKids = eventGuest.EventKids
            };
            _context.Entry(addEventGuest).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                serviceResponse.Status = ServiceResponse.ServiceStatus.Error;
                serviceResponse.Messages.Add("EventGuest Already Exists");
                return serviceResponse;
            }
            serviceResponse.Status = ServiceResponse.ServiceStatus.Updated;
            return serviceResponse;

        }
        public async Task<ServiceResponse> DeleteEventGuest(int id)
        {
            ServiceResponse response = new ServiceResponse();
            var eventGuest = await _context.EventGuests.FindAsync(id);
            if (eventGuest == null)
            {
                response.Status = ServiceResponse.ServiceStatus.NotFound;
                response.Messages.Add("EventGuest Not Found");
                return response;
            }
            try
            {
                _context.EventGuests.Remove(eventGuest);
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