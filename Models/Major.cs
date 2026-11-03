using System.ComponentModel.DataAnnotations;

namespace Group3Blackboard.Models;

public class Major
{
    public int MajorID { get; set; }

    [Required(ErrorMessage = "Please enter a major name.")]
    [StringLength(100)]
    public string Name { get; set; } = string.Empty;

    [Required(ErrorMessage = "Please enter a college.")]
    [StringLength(100)]
    public string College { get; set; } = string.Empty;

    public ICollection<Course> Courses { get; set; } = new List<Course>();
    public ICollection<AppUser> AppUsers { get; set; } = new List<AppUser>();
}
