using Microsoft.AspNetCore.Mvc;

namespace Group3Blackboard.Controllers;

public class CalendarController : Controller
{
    public ContentResult Index() =>
        Content("Student Area - Calendar Controller - Index Action", "text/plain");
}