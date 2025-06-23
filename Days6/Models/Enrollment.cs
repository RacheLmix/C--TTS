using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
namespace Models;

public class Enrollment
{
    public int Id { get; set; }

    [Required]
    public int StudentId { get; set; }

    [Required]
    public int CourseId { get; set; }

    [DataType(DataType.Date)]
    public DateTime EnrollDate { get; set; } = DateTime.Now;

    public Student Student { get; set; }
    public Course Course { get; set; }
}
