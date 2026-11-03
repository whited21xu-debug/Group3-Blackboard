# Group3 Blackboard — Phase 2

This folder is a Phase 2 extension of the corrected Phase 1 project, not a replacement project.

## Local setup

From the folder containing `Group3Blackboard.csproj`:

```powershell
dotnet restore
dotnet build
dotnet ef database update
dotnet run --launch-profile Group3Blackboard
```

If `dotnet ef` is not installed:

```powershell
dotnet tool install --global dotnet-ef --version 10.0.0
dotnet ef database update
```

The SQLite database file is `group3blackboard.db` and is created from the EF Core migration.

## Phase 2 storage distinction

- Majors, users, courses, and reservations: SQLite database through EF Core.
- Temporary student plan: session, serialized as JSON through `SessionExtensions` and accessed through `BlackboardSession`.
- Persistent plan backup: custom two-week cookie containing only Course IDs through `PlanCookie`.
- SSN and date of birth: database only; never session/cookie.

## Demonstration student

Phase 2 intentionally does not add authentication. Student-facing reservation actions use seeded AppUser ID 1 (`Jordan Carter`) as the demonstration student. Authentication/Identity is outside Phase 2.
