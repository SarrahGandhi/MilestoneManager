using System.ComponentModel.DataAnnotations;

namespace Milestonemanager.Models;
public class Event
{
    [Key]
    public int EventId { get; set; }
    [Required]
    [StringLength(100)]
    public string EventName { get; set; }

    [StringLength(100)]
    public string EventLocation { get; set; }


    public DateTime EventDate { get; set; }
    [Required]
    public EventCategory EventCategory { get; set; }
    public ICollection<EventGuest> EventGuests { get; set; }
    public ICollection<EventTask> EventTasks { get; set; }
}
public enum EventCategory
{
    Bride,
    Groom,
    Both
}
public class EventDto
{
    public int EventId { get; set; }
    public string EventName { get; set; }
    public string EventLocation { get; set; }
    public DateTime EventDate { get; set; }
    public EventCategory EventCategory { get; set; }
}
