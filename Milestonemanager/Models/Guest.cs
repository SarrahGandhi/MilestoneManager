using System.ComponentModel.DataAnnotations;

namespace Milestonemanager.Models;
public class Guest
{
    [Key]
    public int GuestId { get; set; }

    [Required]
    [StringLength(100)]
    public string GuestName { get; set; }

    [StringLength(100)]
    public string? GuestLocation { get; set; }

    [StringLength(255)]
    public string? GuestAddress { get; set; }

    [StringLength(15)]
    public string? GuestPhone { get; set; }

    [Required]
    public bool IsInvited { get; set; }

    [StringLength(200)]
    public string? GuestNotes { get; set; }

    [Required]
    public GuestCategory GuestCategory { get; set; }

    public ICollection<EventGuest> EventGuests { get; set; }
}
public enum GuestCategory
{
    Bride,
    Groom,
    Both
}
public class GuestDto
{
    public int GuestId { get; set; }
    public string GuestName { get; set; }
    public string GuestLocation { get; set; }
    public string GuestAddress { get; set; }
    public string GuestPhone { get; set; }
    public bool IsInvited { get; set; }
    public string GuestNotes { get; set; }
    public GuestCategory GuestCategory { get; set; }
}

public class GuestCreateDto
{
    public string GuestName { get; set; }
    public string GuestLocation { get; set; }
    public string GuestAddress { get; set; }
    public string GuestPhone { get; set; }
    public bool IsInvited { get; set; }
    public string GuestNotes { get; set; }
    public GuestCategory GuestCategory { get; set; }

}
