using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Milestonemanager.Models;
public class Admin
{
    [Key]
    public int AdminId { get; set; }
    [Required]
    [StringLength(100)]
    public string AdminName { get; set; }
    [Required]
    [StringLength(100)]
    public string AdminEmail { get; set; }
    [Required]
    [StringLength(15)]


    public string? AdminPhone { get; set; }

    [Required]
    public AdminCategory AdminCategory { get; set; }
    public ICollection<EventTask> EventTasks { get; set; }

}
public enum AdminCategory
{
    SuperAdmin,
    RegularAdmin
}
public class AdminDto
{
    public int AdminId { get; set; }
    public string AdminName { get; set; }
    public string AdminEmail { get; set; }
    public string AdminPhone { get; set; }
    public AdminCategory AdminCategory { get; set; }
}
