using System;
using System.Collections.Generic;

namespace DataAccess.Models;

public partial class TutorSubject
{
    public int TutorSubjectId { get; set; }

    public int? TutorId { get; set; }

    public int? SubjectId { get; set; }

    public decimal? Fee { get; set; }

    public string Details { get; set; }

    public DateTime? CreatedAt { get; set; }

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();

    public virtual Subject Subject { get; set; }

    public virtual User Tutor { get; set; }
}
