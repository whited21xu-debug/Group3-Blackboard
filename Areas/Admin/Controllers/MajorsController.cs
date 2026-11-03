using Group3Blackboard.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Group3Blackboard.Areas.Admin.Controllers;

[Area("Admin")]
public class MajorsController : Controller
{
    private BlackboardContext context;
    public MajorsController(BlackboardContext ctx) => context = ctx;

    public IActionResult Index() => View(context.Majors.OrderBy(m => m.MajorID).ToList());

    [HttpGet]
    public IActionResult Add() => View("Edit", new Major());

    [HttpGet]
    public IActionResult Edit(int id) => View(context.Majors.Find(id) ?? new Major());

    [HttpPost]
    public IActionResult Edit(Major major)
    {
        if (!ModelState.IsValid) return View(major);
        if (major.MajorID == 0) context.Majors.Add(major); else context.Majors.Update(major);
        context.SaveChanges();
        TempData["message"] = "Major saved.";
        return RedirectToAction("Index");
    }

    [HttpGet]
    public IActionResult Delete(int id) => View(context.Majors.Find(id) ?? new Major());

    [HttpPost]
    public IActionResult Delete(Major major)
    {
        try
        {
            context.Majors.Remove(major);
            context.SaveChanges();
            TempData["message"] = "Major deleted.";
        }
        catch (DbUpdateException)
        {
            TempData["message"] = "Major cannot be deleted while courses or users reference it.";
        }
        return RedirectToAction("Index");
    }
}
