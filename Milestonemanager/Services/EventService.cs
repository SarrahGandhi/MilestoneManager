using Microsoft.EntityFrameworkCore;
using Milestonemanager.Data;
using Milestonemanager.Models;
using MilestoneManager.Interfaces;
using MilestoneManager.Models;

namespace CoreEntityFramework.Services
{
    public class EventService : IEventService
    {
        private readonly ApplicationDbContext _context;
        public EventService(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Event>> GetEvents()
        {
            List<Event> events = await _context.Events.ToListAsync();
            List<Event> eventlist = new List<Event>();
            foreach (var event1 in events)
            {
                eventlist.Add(new Event()
                {
                    EventId = event1.EventId,
                    EventName = event1.EventName,
                    EventLocation = event1.EventLocation,
                    EventDate = event1.EventDate,
                    EventCategory = event1.EventCategory
                });
            }
            return eventlist;
        }
        public async Task<EventDto> GetEventById(int id)
        {
            var event1 = await _context.Events.FindAsync(id);
            if (event1 == null)
            {
                return null;
            }
            EventDto events = new EventDto()
            {
                EventName = event1.EventName,
                EventLocation = event1.EventLocation,
                EventDate = event1.EventDate,
                EventCategory = event1.EventCategory
            };
            return events;
        }
        public async Task<EventDto> GetEventByName(string name)
        {
            var event1 = await _context.Events.FirstOrDefaultAsync(x => x.EventName == name);
            if (event1 == null)
            {
                return null;
            }
            EventDto events = new EventDto()
            {
                EventName = event1.EventName,
                EventLocation = event1.EventLocation,
                EventDate = event1.EventDate,
                EventCategory = event1.EventCategory
            };
            return events;
        }
        public async Task<List<Event>> GetEventsByCategory(EventCategory category)
        {
            return await _context.Events
                                 .Where(e => e.EventCategory == category)
                                 .ToListAsync();
        }
        public async Task<List<Event>> GetEventsByDate(DateTime eventDate)
        {
            return await _context.Events.Where(e => e.EventDate == eventDate).ToListAsync();
        }
        public async Task<List<Event>> GetEventsByLocation(string location)
        {
            return await _context.Events.Where(e => e.EventLocation == location).ToListAsync();

        }
        public async Task<ServiceResponse> AddEvent(EventDto eventDto)
        {
            ServiceResponse serviceResponse = new ServiceResponse();
            Event event1 = new Event()
            {
                EventName = eventDto.EventName,
                EventLocation = eventDto.EventLocation,
                EventDate = eventDto.EventDate,
                EventCategory = eventDto.EventCategory
            };
            try
            {
                _context.Events.Add(event1);
                await _context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                serviceResponse.Status = ServiceResponse.ServiceStatus.Error;
                serviceResponse.Messages.Add(e.Message);
            }
            serviceResponse.Status = ServiceResponse.ServiceStatus.Created;
            serviceResponse.CreatedId = event1.EventId;
            return serviceResponse;
        }
        public async Task<ServiceResponse> UpdateEvent(Event event1)
        {
            ServiceResponse serviceResponse = new ServiceResponse();
            Event addEvent = new Event()
            {
                EventName = event1.EventName,
                EventLocation = event1.EventLocation,
                EventDate = event1.EventDate,
                EventCategory = event1.EventCategory
            };
            _context.Entry(addEvent).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                serviceResponse.Status = ServiceResponse.ServiceStatus.Error;
                serviceResponse.Messages.Add("Event Already Exists");
                return serviceResponse;
            }
            serviceResponse.Status = ServiceResponse.ServiceStatus.Updated;
            return serviceResponse;
        }
        public async Task<ServiceResponse> DeleteEvent(int id)
        {
            ServiceResponse response = new ServiceResponse();
            var event1 = await _context.Events.FindAsync(id);
            if (event1 == null)
            {
                response.Status = ServiceResponse.ServiceStatus.NotFound;
                response.Messages.Add("Event Not Found");
                return response;
            }
            try
            {
                _context.Events.Remove(event1);
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