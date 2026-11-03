using Group3Blackboard.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Group3Blackboard.Areas.Admin.Controllers;

[Area("Admin")]
public class CoursesController : Controller
{
    private BlackboardContext context;
    public CoursesController(BlackboardContext ctx) => context = ctx;

    public IActionResult Index()
    {
        var courses = context.Courses.Include(c => c.Major).Include(c => c.Instructor)
            .OrderBy(c => c.CourseNumber).ToList();
        return View(courses);
    }

    [HttpGet]
    public IActionResult Add() => View("Edit", BuildViewModel(new Course()));

    [HttpGet]
    public IActionResult Edit(int id)
    {
        Course course = context.Courses.Find(id) ?? new Course();
        return View(BuildViewModel(course));
    }

    [HttpPost]
    public IActionResult Edit(CourseViewModel model)
    {
        if (!ModelState.IsValid) return View(BuildViewModel(model.Course));
        if (model.Course.CourseID == 0) context.Courses.Add(model.Course); else context.Courses.Update(model.Course);
        context.SaveChanges();
        TempData["message"] = "Course saved.";
        return RedirectToAction("Index");
    }

    [HttpGet]
    public IActionResult Delete(int id)
    {
        Course course = context.Courses.Include(c => c.Major).Include(c => c.Instructor)
            .FirstOrDefault(c => c.CourseID == id) ?? new Course();
        return View(course);
    }

    [HttpPost]
    public IActionResult Delete(Course course)
    {
        try
        {
            context.Courses.Remove(course);
            context.SaveChanges();
            TempData["message"] = "Course deleted.";
        }
        catch (DbUpdateException)
        {
            TempData["message"] = "Course cannot be deleted while reservations reference it.";
        }
        return RedirectToAction("Index");
    }

    private CourseViewModel BuildViewModel(Course course) => new()
    {
        Course = course,
        Majors = context.Majors.OrderBy(m => m.Name).ToList(),
        Instructors = context.AppUsers.Where(u => u.UserType == "Instructor").OrderBy(u => u.Name).ToList()
    };
}
