using Data;
using Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System;
using System.Linq;
namespace Services;

public class StudentService
{
    private readonly AppDbContext _context;

    public StudentService(AppDbContext context)
    {
        _context = context;
    }

    public void AddStudent(Student student)
    {
        _context.Students.Add(student);
        _context.SaveChanges();
    }

    public List<Student> GetAllStudents()
    {
        return _context.Students
            .Include(s => s.Enrollments)
            .ThenInclude(e => e.Course)
            .ToList();
    }

    public Student GetStudentById(int id)
    {
        return _context.Students
            .Include(s => s.Enrollments)
            .ThenInclude(e => e.Course)
            .AsNoTracking()
            .FirstOrDefault(s => s.Id == id);
    }

    public void UpdateStudent(Student student)
    {
        var tracked = _context.Students.Find(student.Id);
        if (tracked != null)
        {
            tracked.FullName = student.FullName;
            tracked.Email = student.Email;
            tracked.BirthDate = student.BirthDate;
            _context.SaveChanges();
        }
    }

    public void DeleteStudent(int id)
    {
        var student = _context.Students.Find(id);
        if (student != null)
        {
            _context.Students.Remove(student);
            _context.SaveChanges();
        }
    }

    public List<Student> FilterStudentsByName(string keyword)
    {
        return _context.Students
            .Where(s => s.FullName.ToLower().Contains(keyword.ToLower()))
            .AsNoTracking()
            .ToList();
    }

    public List<Student> FilterStudentsByBirthDate(DateTime? from, DateTime? to)
    {
        var query = _context.Students.AsQueryable();
        if (from.HasValue)
            query = query.Where(s => s.BirthDate >= from.Value);
        if (to.HasValue)
            query = query.Where(s => s.BirthDate <= to.Value);
        return query.AsNoTracking().ToList();
    }

    public List<Student> GetStudentsSortedByName()
    {
        return _context.Students
            .OrderBy(s => s.FullName)
            .AsNoTracking()
            .ToList();
    }

    public List<Student> GetStudentsByCourse(int courseId)
    {
        return _context.Students
            .Where(s => s.Enrollments.Any(e => e.CourseId == courseId))
            .Include(s => s.Enrollments)
            .ThenInclude(e => e.Course)
            .AsNoTracking()
            .ToList();
    }

    public List<object> GetStudentNamesAndEmails()
    {
        return _context.Students
            .Select(s => new { s.FullName, s.Email })
            .AsNoTracking()
            .ToList<object>();
    }
}
