namespace Group3Blackboard.Models;

public class BlackboardSession
{
    private const string PlanKey = "plan";
    private const string PlanCountKey = "plancount";
    private const string MajorKey = "catalogmajor";
    private const string LocationKey = "cataloglocation";
    private const string SemesterKey = "catalogsemester";

    private ISession session { get; set; }

    public BlackboardSession(ISession session)
    {
        this.session = session;
    }

    public void SetPlan(List<Course> courses)
    {
        // Store only the course data needed by the plan.
        // Do not store EF navigation properties such as Major,
        // Instructor, or Reservations because those relationships
        // can create circular references during JSON serialization.
        List<Course> sessionCourses = courses.Select(c => new Course
        {
            CourseID = c.CourseID,
            CourseNumber = c.CourseNumber,
            CourseName = c.CourseName,
            Credits = c.Credits,
            SemesterOffered = c.SemesterOffered,
            Location = c.Location,
            Hours = c.Hours,
            InstructorID = c.InstructorID,
            MajorID = c.MajorID,
            ShortDescription = c.ShortDescription
        }).ToList();

        session.SetObject(PlanKey, sessionCourses);
        session.SetInt32(PlanCountKey, sessionCourses.Count);
    }

    public List<Course> GetPlan()
    {
        return session.GetObject<List<Course>>(PlanKey)
            ?? new List<Course>();
    }

    public int GetPlanCount()
    {
        return session.GetInt32(PlanCountKey) ?? 0;
    }

    public bool HasPlan()
    {
        return session.GetString(PlanKey) != null;
    }

    public void ClearPlan()
    {
        session.Remove(PlanKey);
        session.Remove(PlanCountKey);
    }

    public void SetCatalogSelections(
        string major,
        string location,
        string semester)
    {
        session.SetString(MajorKey, major);
        session.SetString(LocationKey, location);
        session.SetString(SemesterKey, semester);
    }

    public string GetMajor()
    {
        return session.GetString(MajorKey) ?? "all";
    }

    public string GetLocation()
    {
        return session.GetString(LocationKey) ?? "all";
    }

    public string GetSemester()
    {
        return session.GetString(SemesterKey) ?? "all";
    }
}