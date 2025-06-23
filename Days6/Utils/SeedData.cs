using System;
using System.Collections.Generic;
using System.Linq;
using Data;
using Models;

namespace Utils;

public static class SeedData
{
    public static void Initialize(AppDbContext context)
    {
        if (context.Students.Any() || context.Courses.Any())
        {
            // Nếu đã có dữ liệu thì không seed nữa
            return;
        }

        var students = new List<Student>
        {
            new Student { FullName = "Nguyễn Văn A", Email = "vana@example.com", BirthDate = new DateTime(2000, 1, 1) },
            new Student { FullName = "Trần Thị B", Email = "tranb@example.com", BirthDate = new DateTime(2001, 2, 15) },
            new Student { FullName = "Lê Văn C", Email = "lec@example.com", BirthDate = new DateTime(1999, 9, 10) }
        };

        var courses = new List<Course>
        {
            new Course { Title = "Lập trình C# cơ bản", Level = 2, Duration = 60 },
            new Course { Title = "Cơ sở dữ liệu nâng cao", Level = 4, Duration = 90 },
            new Course { Title = "Thuật toán và cấu trúc dữ liệu", Level = 3, Duration = 75 }
        };

        context.Students.AddRange(students);
        context.Courses.AddRange(courses);
        context.SaveChanges();

        var enrollments = new List<Enrollment>
        {
            new Enrollment { StudentId = students[0].Id, CourseId = courses[0].Id },
            new Enrollment { StudentId = students[1].Id, CourseId = courses[0].Id },
            new Enrollment { StudentId = students[1].Id, CourseId = courses[1].Id },
            new Enrollment { StudentId = students[2].Id, CourseId = courses[2].Id }
        };

        context.Enrollments.AddRange(enrollments);
        context.SaveChanges();
    }
}
