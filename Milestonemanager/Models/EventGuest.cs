using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Milestonemanager.Models;
public class EventGuest
{
    [Key]
    public int GuestEventId { get; set; }
    [ForeignKey("Event")]

    public int EventId { get; set; }
    public virtual Event? Event { get; set; }
    [ForeignKey("Guest")]
    public int GuestId { get; set; }
    public virtual Guest? Guest { get; set; }
    public bool IsRSVPAccepted { get; set; }
    [Required]
    public int EventMen { get; set; }
    [Required]
    public int EventWomen { get; set; }
    [Required]
    public int EventKids { get; set; }

}
public class EventGuestDto
{
    public int GuestEventId { get; set; }
    public int EventId { get; set; }
    public int GuestId { get; set; }
    public bool IsRSVPAccepted { get; set; }
    public int EventMen { get; set; }
    public int EventWomen { get; set; }
    public int EventKids { get; set; }
}