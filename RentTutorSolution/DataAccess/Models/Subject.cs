using System;
using System.Collections.Generic;

namespace DataAccess.Models;

public partial class Subject
{
    public int SubjectId { get; set; }

    public int? CourseId { get; set; }

    public string SubjectCode { get; set; }

    public string SubjectName { get; set; }

    public string Description { get; set; }

    public DateTime? CreatedAt { get; set; }

    public virtual Course Course { get; set; }

    public virtual ICollection<TutorSubject> TutorSubjects { get; set; } = new List<TutorSubject>();
}
