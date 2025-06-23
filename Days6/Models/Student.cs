using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
namespace Models;

public class Student
{
    public int Id { get; set; }

    [Required, MaxLength(100)]
    public string FullName { get; set; }

    [Required, EmailAddress]
    public string Email { get; set; }

    [DataType(DataType.Date)]
    public DateTime BirthDate { get; set; }

    public ICollection<Enrollment> Enrollments { get; set; } = new List<Enrollment>();
}
