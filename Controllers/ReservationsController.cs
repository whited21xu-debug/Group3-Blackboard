using Group3Blackboard.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Group3Blackboard.Controllers;

public class ReservationsController : Controller
{
    private const int DemoStudentID = 1;
    private BlackboardContext context;
    public ReservationsController(BlackboardContext ctx) => context = ctx;

    [HttpGet]
    public IActionResult Index()
    {
        var reservations = context.Reservations
            .Include(r => r.Course)
                .ThenInclude(c => c.Major)
            .Include(r => r.Course)
                .ThenInclude(c => c.Instructor)
            .Where(r => r.AppUserID == DemoStudentID)
            .OrderByDescending(r => r.ReservationDate)
            .ToList();
        return View(reservations);
    }

    [HttpPost]
    public IActionResult Reserve(int id)
    {
        var session = new BlackboardSession(HttpContext.Session);
        RestorePlanFromCookie(session);
        List<Course> plan = session.GetPlan();
        Course? course = plan.FirstOrDefault(c => c.CourseID == id);

        if (course == null)
        {
            TempData["message"] = "That course is not in your plan.";
            return RedirectToAction("Index", "Plan");
        }

        bool alreadyReserved = context.Reservations.Any(r => r.AppUserID == DemoStudentID && r.CourseID == id);
        if (!alreadyReserved)
        {
            context.Reservations.Add(new Reservation
            {
                AppUserID = DemoStudentID,
                CourseID = id,
                ReservationDate = DateTime.Now
            });
            context.SaveChanges();
        }

        plan.Remove(course);
        session.SetPlan(plan);
        new PlanCookie(Response.Cookies).SetCourseIDs(plan.Select(c => c.CourseID));
        TempData["message"] = alreadyReserved ? "Course was already reserved; it was removed from your plan." : $"Reservation completed for {course.CourseNumber}.";
        return RedirectToAction("Index");
    }

    private void RestorePlanFromCookie(BlackboardSession session)
    {
        if (session.HasPlan()) return;
        List<int> ids = new PlanCookie(Request.Cookies).GetCourseIDs();
        if (ids.Count == 0) return;
        List<Course> courses = context.Courses.Include(c => c.Major).Include(c => c.Instructor)
            .Where(c => ids.Contains(c.CourseID)).ToList();
        courses = ids.Join(courses, id => id, c => c.CourseID, (id, c) => c).ToList();
        session.SetPlan(courses);
    }
}
