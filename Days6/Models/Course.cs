using System.Collections.Generic;
using System;
using System.ComponentModel.DataAnnotations;
namespace Models;

public class Course
{
    public int Id { get; set; }

    [Required, MaxLength(100)]
    public string Title { get; set; }

    [Range(1, 5)]
    public int Level { get; set; }

    [Range(1, 365)]
    public int Duration { get; set; }

    public ICollection<Enrollment> Enrollments { get; set; } = new List<Enrollment>();
}
