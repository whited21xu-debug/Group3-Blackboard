using Group3Blackboard.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Group3Blackboard.Areas.Admin.Controllers;

[Area("Admin")]
public class UsersController : Controller
{
    private BlackboardContext context;
    public UsersController(BlackboardContext ctx) => context = ctx;

    public IActionResult Index()
    {
        var users = context.AppUsers.Include(u => u.Major).OrderBy(u => u.Name).ToList();
        return View(users);
    }

    [HttpGet]
    public IActionResult Add() => View("Edit", BuildViewModel(new AppUser()));

    [HttpGet]
    public IActionResult Edit(int id)
    {
        AppUser user = context.AppUsers.Find(id) ?? new AppUser();
        return View(BuildViewModel(user));
    }

    [HttpPost]
    public IActionResult Edit(UserViewModel model)
    {
        if (!ModelState.IsValid) return View(BuildViewModel(model.AppUser));
        if (model.AppUser.AppUserID == 0) context.AppUsers.Add(model.AppUser); else context.AppUsers.Update(model.AppUser);
        context.SaveChanges();
        TempData["message"] = "User saved.";
        return RedirectToAction("Index");
    }

    [HttpGet]
    public IActionResult Delete(int id)
    {
        AppUser user = context.AppUsers.Include(u => u.Major).FirstOrDefault(u => u.AppUserID == id) ?? new AppUser();
        return View(user);
    }

    [HttpPost]
    public IActionResult Delete(AppUser appUser)
    {
        try
        {
            context.AppUsers.Remove(appUser);
            context.SaveChanges();
            TempData["message"] = "User deleted.";
        }
        catch (DbUpdateException)
        {
            TempData["message"] = "User cannot be deleted while courses or reservations reference the user.";
        }
        return RedirectToAction("Index");
    }

    private UserViewModel BuildViewModel(AppUser user) => new()
    {
        AppUser = user,
        Majors = context.Majors.OrderBy(m => m.Name).ToList()
    };
}
