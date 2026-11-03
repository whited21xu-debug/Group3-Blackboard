using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Group3Blackboard.Migrations
{
    /// <inheritdoc />
    public partial class InitialPhase2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Majors",
                columns: table => new
                {
                    MajorID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    College = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Majors", x => x.MajorID);
                });

            migrationBuilder.CreateTable(
                name: "AppUsers",
                columns: table => new
                {
                    AppUserID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    EmailAddress = table.Column<string>(type: "TEXT", nullable: false),
                    MajorID = table.Column<int>(type: "INTEGER", nullable: true),
                    SSN = table.Column<string>(type: "TEXT", nullable: false),
                    DateOfBirth = table.Column<DateTime>(type: "TEXT", nullable: false),
                    UserType = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppUsers", x => x.AppUserID);
                    table.ForeignKey(
                        name: "FK_AppUsers_Majors_MajorID",
                        column: x => x.MajorID,
                        principalTable: "Majors",
                        principalColumn: "MajorID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Courses",
                columns: table => new
                {
                    CourseID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    CourseNumber = table.Column<string>(type: "TEXT", maxLength: 20, nullable: false),
                    CourseName = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    Credits = table.Column<int>(type: "INTEGER", nullable: false),
                    SemesterOffered = table.Column<string>(type: "TEXT", nullable: false),
                    Location = table.Column<string>(type: "TEXT", nullable: false),
                    Hours = table.Column<string>(type: "TEXT", maxLength: 80, nullable: false),
                    InstructorID = table.Column<int>(type: "INTEGER", nullable: false),
                    MajorID = table.Column<int>(type: "INTEGER", nullable: false),
                    ShortDescription = table.Column<string>(type: "TEXT", maxLength: 500, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Courses", x => x.CourseID);
                    table.ForeignKey(
                        name: "FK_Courses_AppUsers_InstructorID",
                        column: x => x.InstructorID,
                        principalTable: "AppUsers",
                        principalColumn: "AppUserID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Courses_Majors_MajorID",
                        column: x => x.MajorID,
                        principalTable: "Majors",
                        principalColumn: "MajorID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Reservations",
                columns: table => new
                {
                    ReservationID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    AppUserID = table.Column<int>(type: "INTEGER", nullable: false),
                    CourseID = table.Column<int>(type: "INTEGER", nullable: false),
                    ReservationDate = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reservations", x => x.ReservationID);
                    table.ForeignKey(
                        name: "FK_Reservations_AppUsers_AppUserID",
                        column: x => x.AppUserID,
                        principalTable: "AppUsers",
                        principalColumn: "AppUserID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Reservations_Courses_CourseID",
                        column: x => x.CourseID,
                        principalTable: "Courses",
                        principalColumn: "CourseID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "Majors",
                columns: new[] { "MajorID", "College", "Name" },
                values: new object[,]
                {
                    { 1, "College of Business", "Business Administration" },
                    { 2, "College of Arts and Sciences", "Computer Science" },
                    { 3, "College of Education", "Elementary Education" },
                    { 4, "College of Health and Human Services", "Health Administration" },
                    { 5, "College of Business", "Information Systems" },
                    { 6, "College of Arts and Sciences", "Mathematics" }
                });

            migrationBuilder.InsertData(
                table: "AppUsers",
                columns: new[] { "AppUserID", "DateOfBirth", "EmailAddress", "MajorID", "Name", "SSN", "UserType" },
                values: new object[,]
                {
                    { 1, new DateTime(2003, 4, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), "jcarter@example.edu", 2, "Jordan Carter", "000-00-0001", "Student" },
                    { 2, new DateTime(1980, 2, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "abrooks@example.edu", 2, "Dr. Avery Brooks", "000-00-0002", "Instructor" },
                    { 3, new DateTime(1978, 8, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), "mlee@example.edu", 1, "Prof. Morgan Lee", "000-00-0003", "Instructor" },
                    { 4, new DateTime(1985, 11, 21, 0, 0, 0, 0, DateTimeKind.Unspecified), "treed@example.edu", 6, "Dr. Taylor Reed", "000-00-0004", "Instructor" },
                    { 5, new DateTime(2002, 6, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), "cnguyen@example.edu", 5, "Casey Nguyen", "000-00-0005", "Student" }
                });

            migrationBuilder.InsertData(
                table: "Courses",
                columns: new[] { "CourseID", "CourseName", "CourseNumber", "Credits", "Hours", "InstructorID", "Location", "MajorID", "SemesterOffered", "ShortDescription" },
                values: new object[,]
                {
                    { 1, "Introduction to Programming", "CPSC-2005", 3, "MW 10:00 AM - 11:15 AM", 2, "University Park", 2, "Fall", "Programming fundamentals and problem solving." },
                    { 2, "Internet Programming", "CPSC-3090", 3, "T 6:00 PM - 8:50 PM", 2, "Hybrid", 2, "Fall", "Web application development with ASP.NET Core MVC." },
                    { 3, "Business Systems Analysis", "MIS-3101", 3, "Online asynchronous", 3, "Online", 5, "Spring", "Analysis and design of business information systems." },
                    { 4, "Principles of Management", "MGMT-2100", 3, "TR 1:00 PM - 2:15 PM", 3, "University Park", 1, "Spring", "Foundations of management and organizational practice." },
                    { 5, "Applied Statistics", "MATH-2281", 3, "Online asynchronous", 4, "Online", 6, "Summer", "Descriptive and inferential statistics for applications." },
                    { 6, "Database Systems", "CPSC-4190", 3, "MW 4:30 PM - 5:45 PM", 2, "University Park", 2, "Spring", "Relational database design, SQL, and data applications." }
                });

            migrationBuilder.CreateIndex(
                name: "IX_AppUsers_MajorID",
                table: "AppUsers",
                column: "MajorID");

            migrationBuilder.CreateIndex(
                name: "IX_Courses_InstructorID",
                table: "Courses",
                column: "InstructorID");

            migrationBuilder.CreateIndex(
                name: "IX_Courses_MajorID",
                table: "Courses",
                column: "MajorID");

            migrationBuilder.CreateIndex(
                name: "IX_Reservations_AppUserID",
                table: "Reservations",
                column: "AppUserID");

            migrationBuilder.CreateIndex(
                name: "IX_Reservations_CourseID",
                table: "Reservations",
                column: "CourseID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Reservations");

            migrationBuilder.DropTable(
                name: "Courses");

            migrationBuilder.DropTable(
                name: "AppUsers");

            migrationBuilder.DropTable(
                name: "Majors");
        }
    }
}
