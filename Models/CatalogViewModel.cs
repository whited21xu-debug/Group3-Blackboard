namespace Group3Blackboard.Models;

public class CatalogViewModel
{
    public List<Course> Courses { get; set; } = new();
    public List<Major> Majors { get; set; } = new();
    public string Major { get; set; } = "all";
    public string Location { get; set; } = "all";
    public string Semester { get; set; } = "all";
}
