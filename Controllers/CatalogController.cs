using Group3Blackboard.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Group3Blackboard.Controllers;

public class CatalogController : Controller
{
    private BlackboardContext context;
    public CatalogController(BlackboardContext ctx) => context = ctx;

    [HttpGet]
    public IActionResult Index(string? major, string? location, string? semester)
    {
        var session = new BlackboardSession(HttpContext.Session);

        major = string.IsNullOrEmpty(major) ? session.GetMajor() : major;
        location = string.IsNullOrEmpty(location) ? session.GetLocation() : location;
        semester = string.IsNullOrEmpty(semester) ? session.GetSemester() : semester;
        session.SetCatalogSelections(major, location, semester);

        IQueryable<Course> query = context.Courses
            .Include(c => c.Major)
            .Include(c => c.Instructor)
            .OrderBy(c => c.CourseNumber);

        if (major != "all") query = query.Where(c => c.MajorID.ToString() == major);
        if (location != "all") query = query.Where(c => c.Location == location);
        if (semester != "all") query = query.Where(c => c.SemesterOffered == semester);

        var model = new CatalogViewModel
        {
            Courses = query.ToList(),
            Majors = context.Majors.OrderBy(m => m.Name).ToList(),
            Major = major,
            Location = location,
            Semester = semester
        };
        return View(model);
    }
}
