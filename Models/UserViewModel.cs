namespace Group3Blackboard.Models;

public class UserViewModel
{
    public AppUser AppUser { get; set; } = new();
    public List<Major> Majors { get; set; } = new();
}
