using System;
using System.Collections.Generic;
using System.Linq;
using Data;
using Models;
using Microsoft.EntityFrameworkCore;

namespace Services;

public class EnrollmentService
{
    private readonly AppDbContext _context;

    public EnrollmentService(AppDbContext context)
    {
        _context = context;
    }

    public void EnrollStudent(int studentId, int courseId)
    {
        var enrollment = new Enrollment
        {
            StudentId = studentId,
            CourseId = courseId,
            EnrollDate = DateTime.Now
        };

        _context.Enrollments.Add(enrollment);
        _context.SaveChanges();
    }

    public Enrollment GetEnrollmentById(int id)
    {
        return _context.Enrollments
            .Include(e => e.Student)
            .Include(e => e.Course)
            .AsNoTracking()
            .FirstOrDefault(e => e.Id == id);
    }

    public void UpdateEnrollment(Enrollment enrollment)
    {
        _context.Enrollments.Update(enrollment);
        _context.SaveChanges();
    }

    public void DeleteEnrollment(int id)
    {
        var enrollment = _context.Enrollments.Find(id);
        if (enrollment != null)
        {
            _context.Enrollments.Remove(enrollment);
            _context.SaveChanges();
        }
    }
}
