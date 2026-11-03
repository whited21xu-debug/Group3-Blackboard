using Microsoft.EntityFrameworkCore;

namespace Group3Blackboard.Models;

public class BlackboardContext : DbContext
{
    public BlackboardContext(DbContextOptions<BlackboardContext> options) : base(options) { }

    public DbSet<Major> Majors { get; set; } = null!;
    public DbSet<AppUser> AppUsers { get; set; } = null!;
    public DbSet<Course> Courses { get; set; } = null!;
    public DbSet<Reservation> Reservations { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Course>()
            .HasOne(c => c.Instructor)
            .WithMany(u => u.CoursesTaught)
            .HasForeignKey(c => c.InstructorID)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Course>()
            .HasOne(c => c.Major)
            .WithMany(m => m.Courses)
            .HasForeignKey(c => c.MajorID)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<AppUser>()
            .HasOne(u => u.Major)
            .WithMany(m => m.AppUsers)
            .HasForeignKey(u => u.MajorID)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Reservation>()
            .HasOne(r => r.AppUser)
            .WithMany(u => u.Reservations)
            .HasForeignKey(r => r.AppUserID)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Reservation>()
            .HasOne(r => r.Course)
            .WithMany(c => c.Reservations)
            .HasForeignKey(r => r.CourseID)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Major>().HasData(
            new Major { MajorID = 1, Name = "Business Administration", College = "College of Business" },
            new Major { MajorID = 2, Name = "Computer Science", College = "College of Arts and Sciences" },
            new Major { MajorID = 3, Name = "Elementary Education", College = "College of Education" },
            new Major { MajorID = 4, Name = "Health Administration", College = "College of Health and Human Services" },
            new Major { MajorID = 5, Name = "Information Systems", College = "College of Business" },
            new Major { MajorID = 6, Name = "Mathematics", College = "College of Arts and Sciences" }
        );

        modelBuilder.Entity<AppUser>().HasData(
            new AppUser { AppUserID = 1, Name = "Jordan Carter", EmailAddress = "jcarter@example.edu", MajorID = 2, SSN = "000-00-0001", DateOfBirth = new DateTime(2003, 4, 12), UserType = "Student" },
            new AppUser { AppUserID = 2, Name = "Dr. Avery Brooks", EmailAddress = "abrooks@example.edu", MajorID = 2, SSN = "000-00-0002", DateOfBirth = new DateTime(1980, 2, 15), UserType = "Instructor" },
            new AppUser { AppUserID = 3, Name = "Prof. Morgan Lee", EmailAddress = "mlee@example.edu", MajorID = 1, SSN = "000-00-0003", DateOfBirth = new DateTime(1978, 8, 9), UserType = "Instructor" },
            new AppUser { AppUserID = 4, Name = "Dr. Taylor Reed", EmailAddress = "treed@example.edu", MajorID = 6, SSN = "000-00-0004", DateOfBirth = new DateTime(1985, 11, 21), UserType = "Instructor" },
            new AppUser { AppUserID = 5, Name = "Casey Nguyen", EmailAddress = "cnguyen@example.edu", MajorID = 5, SSN = "000-00-0005", DateOfBirth = new DateTime(2002, 6, 3), UserType = "Student" }
        );

        modelBuilder.Entity<Course>().HasData(
            new Course { CourseID = 1, CourseNumber = "CPSC-2005", CourseName = "Introduction to Programming", Credits = 3, SemesterOffered = "Fall", Location = "University Park", Hours = "MW 10:00 AM - 11:15 AM", InstructorID = 2, MajorID = 2, ShortDescription = "Programming fundamentals and problem solving." },
            new Course { CourseID = 2, CourseNumber = "CPSC-3090", CourseName = "Internet Programming", Credits = 3, SemesterOffered = "Fall", Location = "Hybrid", Hours = "T 6:00 PM - 8:50 PM", InstructorID = 2, MajorID = 2, ShortDescription = "Web application development with ASP.NET Core MVC." },
            new Course { CourseID = 3, CourseNumber = "MIS-3101", CourseName = "Business Systems Analysis", Credits = 3, SemesterOffered = "Spring", Location = "Online", Hours = "Online asynchronous", InstructorID = 3, MajorID = 5, ShortDescription = "Analysis and design of business information systems." },
            new Course { CourseID = 4, CourseNumber = "MGMT-2100", CourseName = "Principles of Management", Credits = 3, SemesterOffered = "Spring", Location = "University Park", Hours = "TR 1:00 PM - 2:15 PM", InstructorID = 3, MajorID = 1, ShortDescription = "Foundations of management and organizational practice." },
            new Course { CourseID = 5, CourseNumber = "MATH-2281", CourseName = "Applied Statistics", Credits = 3, SemesterOffered = "Summer", Location = "Online", Hours = "Online asynchronous", InstructorID = 4, MajorID = 6, ShortDescription = "Descriptive and inferential statistics for applications." },
            new Course { CourseID = 6, CourseNumber = "CPSC-4190", CourseName = "Database Systems", Credits = 3, SemesterOffered = "Spring", Location = "University Park", Hours = "MW 4:30 PM - 5:45 PM", InstructorID = 2, MajorID = 2, ShortDescription = "Relational database design, SQL, and data applications." }
        );
    }
}
