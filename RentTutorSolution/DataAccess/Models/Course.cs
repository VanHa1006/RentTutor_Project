using System;
using System.Collections.Generic;

namespace DataAccess.Models;

public partial class Course
{
    public int CourseId { get; set; }

    public string CourseName { get; set; }

    public string Description { get; set; }

    public DateTime? CreatedAt { get; set; }

    public virtual ICollection<Subject> Subjects { get; set; } = new List<Subject>();
}
