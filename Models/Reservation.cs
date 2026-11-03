using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace Group3Blackboard.Models;

public class Reservation
{
    public int ReservationID { get; set; }
    public int AppUserID { get; set; }
    [ValidateNever]
    public AppUser AppUser { get; set; } = null!;
    public int CourseID { get; set; }
    [ValidateNever]
    public Course Course { get; set; } = null!;
    public DateTime ReservationDate { get; set; }
}
