using Microsoft.AspNetCore.Mvc;

namespace Group3Blackboard.Controllers;

public class HomeController : Controller
{
    public IActionResult Index() => View();
}
