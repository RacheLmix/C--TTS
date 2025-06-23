using System;
using System.Collections.Generic;
using System.Linq;
using Data;
using Models;
using Microsoft.EntityFrameworkCore;
namespace Services;

public class CourseService
{
    private readonly AppDbContext _context;

    public CourseService(AppDbContext context)
    {
        _context = context;
    }

    public void AddCourse(Course course)
    {
        _context.Courses.Add(course);
        _context.SaveChanges();
    }

    public List<Course> GetAllCourses()
    {
        return _context.Courses.ToList();
    }

    public Course GetCourseById(int id)
    {
        return _context.Courses
            .AsNoTracking()
            .FirstOrDefault(c => c.Id == id);
    }

    public void UpdateCourse(Course course)
    {
        var tracked = _context.Courses.Find(course.Id);
        if (tracked != null)
        {
            tracked.Title = course.Title;
            tracked.Level = course.Level;
            tracked.Duration = course.Duration;
            _context.SaveChanges();
        }
    }

    public void DeleteCourse(int id)
    {
        var course = _context.Courses.Find(id);
        if (course != null)
        {
            _context.Courses.Remove(course);
            _context.SaveChanges();
        }
    }

    public List<Course> GetCoursesWithMoreThan5Students()
    {
        return _context.Courses
            .Where(c => c.Enrollments.Count > 5)
            .AsNoTracking()
            .ToList();
    }

    public List<object> GetCourseTitlesAndLevels()
    {
        return _context.Courses
            .Select(c => new { c.Title, c.Level })
            .AsNoTracking()
            .ToList<object>();
    }
}
