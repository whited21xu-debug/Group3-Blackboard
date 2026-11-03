namespace Group3Blackboard.Models;

public class CourseViewModel
{
    public Course Course { get; set; } = new();
    public List<Major> Majors { get; set; } = new();
    public List<AppUser> Instructors { get; set; } = new();
}
