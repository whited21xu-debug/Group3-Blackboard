# Manual Phase 2 reproduction steps

These steps reproduce the changes made to the corrected Phase 1 project.

1. Keep the Phase 1 project and its Student/default area, Admin area, three-layout chain (`_Layout`, `_MainLayout`, `_AdminLayout`), `_ViewStart`, Bootstrap, and routes.
2. Add EF Core 10 SQLite and Design package references to `Group3Blackboard.csproj`.
3. Add `ConnectionStrings:BlackboardContext` with `Data Source=group3blackboard.db` to `appsettings.json`.
4. Create `Models/Major.cs`, `AppUser.cs`, `Course.cs`, and `Reservation.cs`. Use explicit FK properties and navigation properties. Keep fixed `UserTypes`, `Semesters`, and `Locations` as static lists.
5. Create `Models/BlackboardContext.cs` with `DbSet` properties, relationship configuration using `DeleteBehavior.Restrict`, and fictional seed data for majors, users, and courses.
6. Create `CatalogViewModel.cs`, `CourseViewModel.cs`, and `UserViewModel.cs` in `Models` for pages that need entity data plus choices/filter data.
7. Copy the Chapter 9 JSON-session idea into `Models/SessionExtensions.cs` using `System.Text.Json`.
8. Create `Models/BlackboardSession.cs`. Centralize the keys for plan, plan count, and catalog filter selections. Provide get/set/count/clear methods.
9. Create `Models/PlanCookie.cs` with two constructors: one accepts `IRequestCookieCollection` for reads; one accepts `IResponseCookies` for writes/deletes. Store only dash-separated Course IDs and expire the cookie after 14 days.
10. Update `Program.cs`: call `AddMemoryCache`, `AddSession` with an explicit idle timeout, `AddControllersWithViews`, and `AddDbContext<BlackboardContext>(UseSqlite(...))`. Call `UseSession()` after `UseRouting()` and before route mapping. Preserve Admin and default routes.
11. Replace the Catalog placeholder with `CatalogController.Index`. Query Courses with `Include(c => c.Major)` and `Include(c => c.Instructor)`, filter by Major/Location/Semester, retain selections in `BlackboardSession`, and pass a `CatalogViewModel` to a strongly typed view.
12. Create `PlanController`. On GET, if the session has no plan but the custom cookie has Course IDs, query those courses and rebuild session. On Add/Remove/Clear POST, update session and the persistent cookie, set TempData, and redirect.
13. Create `ReservationsController`. For the Phase 2 demo, use seeded student AppUser ID 1 because authentication is explicitly out of scope. A Reserve POST inserts a Reservation, removes that course from plan, updates session/cookie, sets TempData, and redirects.
14. Convert Home and Calendar placeholders to simple Razor views. Calendar stays intentionally minimal because Phase 2 gives no new calendar behavior.
15. In Admin, keep `MajorsController` and `CoursesController` but replace placeholders with CRUD. Add `UsersController`; remove `StudentsController`. Every successful POST sets TempData and redirects. Validation failures redisplay the form.
16. For Course Add/Edit, use `CourseViewModel` so the form gets Major and Instructor dropdowns. Only AppUsers whose UserType is Instructor appear in the Instructor dropdown.
17. For User Add/Edit, use `UserViewModel` so the form gets the Major dropdown. Use `AppUser.UserTypes` for the fixed Student/Instructor dropdown.
18. Add strongly typed Admin views for Major, Course, and User Index/Edit/Delete. Use tag helpers, Bootstrap, validation helpers, and responsive tables.
19. Update `_MainLayout.cshtml` to show Home, Catalog, Calendar, Reservations and a plan-count badge. The view reads session through `Context.Session` and `BlackboardSession`.
20. Update `_AdminLayout.cshtml` to show Major, Course, User and preserve active-link highlighting and responsive navigation.
21. Put the TempData alert only in `Views/Shared/_Layout.cshtml`, so both Student and Admin layouts inherit the same one-time message location.
22. Add the migration under `Migrations`. On a machine with the .NET 10 SDK and EF CLI, run `dotnet restore`, `dotnet build`, and `dotnet ef database update`.
23. Test: Admin CRUD; Catalog filters; add plan; verify plan count; inspect session/custom cookie; delete the session cookie and reload Plan to demonstrate restoration; reserve a planned course; confirm it leaves session/cookie and appears in Reservations/database.

## Assumptions made

- The screenshots show the required flow/fields but not a login system. Because authentication/Identity is prohibited in Phase 2, seeded AppUser ID 1 is the demonstration student for reservations.
- The professor's screenshots do not clearly prescribe exact values for Semester and Location. This implementation uses Fall/Spring/Summer and University Park/Online/Hybrid as fixed model lists.
- Calendar remains a simple page because the Phase 2 notes require the navigation item but do not define new calendar behavior.
- The plan cookie contains only Course IDs. Course details are re-queried from SQLite when session restoration is needed.
