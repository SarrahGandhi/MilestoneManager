using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Milestonemanager.Models;
public class EventTask
{
    [Key]
    public int TaskId { get; set; }
    [Required]
    [StringLength(100)]
    public string TaskName { get; set; }
    [StringLength(255)]

    public string TaskDescription { get; set; }

    public DateTime DueDate { get; set; }
    [ForeignKey("Event")]
    public int EventId { get; set; }
    [ForeignKey("Admin")]
    public int AdminId { get; set; }
    [DefaultValue(false)]
    public bool IsCompleted { get; set; }
    [Required]
    public EventTaskCategory EventTaskCategory { get; set; }

}
public enum EventTaskCategory
{
    Bride,
    Groom,
    Both
}
public class EventTaskDto
{
    public int TaskId { get; set; }
    public string TaskName { get; set; }
    public string TaskDescription { get; set; }
    public DateTime DueDate { get; set; }
    public int EventId { get; set; }
    public int AdminId { get; set; }
    public bool IsCompleted { get; set; }
    public EventTaskCategory EventTaskCategory { get; set; }
}
public class EventTaskListDto
{
    public int TaskId { get; set; }
    public string TaskName { get; set; }

    public string AdminName { get; set; }
    public string EventName { get; set; }
    public string TaskDescription { get; set; }
    public DateTime DueDate { get; set; }
    public bool IsCompleted { get; set; }
    public EventTaskCategory EventTaskCategory { get; set; }
}
