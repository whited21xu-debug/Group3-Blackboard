using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace Group3Blackboard.Models;

public class Course
{
    public int CourseID { get; set; }

    [Required(ErrorMessage = "Please enter a course number.")]
    [StringLength(20)]
    public string CourseNumber { get; set; } = string.Empty;

    [Required(ErrorMessage = "Please enter a course name.")]
    [StringLength(100)]
    public string CourseName { get; set; } = string.Empty;

    [Range(1, 6, ErrorMessage = "Credits must be from 1 through 6.")]
    public int Credits { get; set; }

    [Required(ErrorMessage = "Please select a semester.")]
    public string SemesterOffered { get; set; } = string.Empty;

    [Required(ErrorMessage = "Please select a location.")]
    public string Location { get; set; } = string.Empty;

    [Required(ErrorMessage = "Please enter course hours/time.")]
    [StringLength(80)]
    public string Hours { get; set; } = string.Empty;

    [Range(1, int.MaxValue, ErrorMessage = "Please select an instructor.")]
    public int InstructorID { get; set; }
    [ValidateNever]
    public AppUser Instructor { get; set; } = null!;

    [Range(1, int.MaxValue, ErrorMessage = "Please select a major.")]
    public int MajorID { get; set; }
    [ValidateNever]
    public Major Major { get; set; } = null!;

    [Required(ErrorMessage = "Please enter a short description.")]
    [StringLength(500)]
    public string ShortDescription { get; set; } = string.Empty;

    public static readonly List<string> Semesters = new() { "Fall", "Spring", "Summer" };
    public static readonly List<string> Locations = new() { "University Park", "Online", "Hybrid" };

    public ICollection<Reservation> Reservations { get; set; } = new List<Reservation>();
}
