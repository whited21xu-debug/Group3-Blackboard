using Group3Blackboard.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Group3Blackboard.Controllers;

public class PlanController : Controller
{
    private BlackboardContext context;
    public PlanController(BlackboardContext ctx) => context = ctx;

    [HttpGet]
    public IActionResult Index()
    {
        var session = new BlackboardSession(HttpContext.Session);
        RestorePlanFromCookie(session);
        return View(session.GetPlan());
    }

    [HttpPost]
    public IActionResult Add(int id)
    {
        var session = new BlackboardSession(HttpContext.Session);
        RestorePlanFromCookie(session);
        List<Course> plan = session.GetPlan();

        Course? course = context.Courses
            .Include(c => c.Major)
            .Include(c => c.Instructor)
            .FirstOrDefault(c => c.CourseID == id);

        if (course == null)
        {
            TempData["message"] = "Course not found.";
            return RedirectToAction("Index", "Catalog");
        }

        if (!plan.Any(c => c.CourseID == id))
        {
            plan.Add(course);
            session.SetPlan(plan);
            new PlanCookie(Response.Cookies).SetCourseIDs(plan.Select(c => c.CourseID));
            TempData["message"] = $"{course.CourseNumber} added to your plan.";
        }
        else
        {
            TempData["message"] = $"{course.CourseNumber} is already in your plan.";
        }

        return RedirectToAction("Index", "Catalog", new
        {
            major = session.GetMajor(),
            location = session.GetLocation(),
            semester = session.GetSemester()
        });
    }

    [HttpPost]
    public IActionResult Remove(int id)
    {
        var session = new BlackboardSession(HttpContext.Session);
        RestorePlanFromCookie(session);
        List<Course> plan = session.GetPlan();
        Course? course = plan.FirstOrDefault(c => c.CourseID == id);
        if (course != null) plan.Remove(course);
        session.SetPlan(plan);
        new PlanCookie(Response.Cookies).SetCourseIDs(plan.Select(c => c.CourseID));
        TempData["message"] = "Course removed from your plan.";
        return RedirectToAction("Index");
    }

    [HttpPost]
    public IActionResult Clear()
    {
        var session = new BlackboardSession(HttpContext.Session);
        session.ClearPlan();
        new PlanCookie(Response.Cookies).Remove();
        TempData["message"] = "Plan cleared.";
        return RedirectToAction("Index");
    }

    private void RestorePlanFromCookie(BlackboardSession session)
    {
        if (session.HasPlan()) return;

        List<int> ids = new PlanCookie(Request.Cookies).GetCourseIDs();
        if (ids.Count == 0) return;

        List<Course> courses = context.Courses
            .Include(c => c.Major)
            .Include(c => c.Instructor)
            .Where(c => ids.Contains(c.CourseID))
            .ToList();
        courses = ids.Join(courses, id => id, c => c.CourseID, (id, c) => c).ToList();
        session.SetPlan(courses);
    }
}
