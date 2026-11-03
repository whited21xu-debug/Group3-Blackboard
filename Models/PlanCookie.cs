namespace Group3Blackboard.Models;

public class PlanCookie
{
    private const string CookieName = "Group3BlackboardPlan";
    private IRequestCookieCollection? requestCookies;
    private IResponseCookies? responseCookies;

    // Used when reading the persistent plan cookie from Request.Cookies.
    public PlanCookie(IRequestCookieCollection cookies) => requestCookies = cookies;

    // Used when writing or deleting the persistent plan cookie through Response.Cookies.
    public PlanCookie(IResponseCookies cookies) => responseCookies = cookies;

    public List<int> GetCourseIDs()
    {
        string? value = requestCookies?[CookieName];
        if (string.IsNullOrEmpty(value)) return new List<int>();

        return value.Split('-', StringSplitOptions.RemoveEmptyEntries)
            .Select(id => int.TryParse(id, out int courseID) ? courseID : 0)
            .Where(id => id > 0)
            .Distinct()
            .ToList();
    }

    public void SetCourseIDs(IEnumerable<int> courseIDs)
    {
        string value = string.Join('-', courseIDs.Distinct());
        if (string.IsNullOrEmpty(value))
        {
            Remove();
            return;
        }

        var options = new CookieOptions
        {
            Expires = DateTimeOffset.Now.AddDays(14),
            HttpOnly = true,
            IsEssential = true,
            SameSite = SameSiteMode.Lax
        };
        responseCookies?.Append(CookieName, value, options);
    }

    public void Remove() => responseCookies?.Delete(CookieName);
}
