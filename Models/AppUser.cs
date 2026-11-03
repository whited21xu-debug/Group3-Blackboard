using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace Group3Blackboard.Models;

public class AppUser
{
    public int AppUserID { get; set; }

    [Required(ErrorMessage = "Please enter a name.")]
    [StringLength(100)]
    public string Name { get; set; } = string.Empty;

    [Required(ErrorMessage = "Please enter an email address.")]
    [EmailAddress]
    public string EmailAddress { get; set; } = string.Empty;

    public int? MajorID { get; set; }
    [ValidateNever]
    public Major? Major { get; set; }

    [Required(ErrorMessage = "Please enter an SSN.")]
    [RegularExpression(@"^\d{3}-\d{2}-\d{4}$", ErrorMessage = "Use SSN format 000-00-0000.")]
    public string SSN { get; set; } = string.Empty;

    [Required(ErrorMessage = "Please enter a date of birth.")]
    [DataType(DataType.Date)]
    public DateTime? DateOfBirth { get; set; }

    [Required(ErrorMessage = "Please select a user type.")]
    public string UserType { get; set; } = string.Empty;

    public static readonly List<string> UserTypes = new() { "Student", "Instructor" };

    public ICollection<Course> CoursesTaught { get; set; } = new List<Course>();
    public ICollection<Reservation> Reservations { get; set; } = new List<Reservation>();
}
