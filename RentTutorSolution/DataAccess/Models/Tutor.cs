using System;
using System.Collections.Generic;

namespace DataAccess.Models;

public partial class Tutor
{
    public int TutorId { get; set; }

    public string Qualifications { get; set; }

    public string Experience { get; set; }

    public string Specialization { get; set; }

    public virtual ICollection<Course> Courses { get; set; } = new List<Course>();

    public virtual User TutorNavigation { get; set; }

    public virtual ICollection<UserApprovalLog> UserApprovalLogs { get; set; } = new List<UserApprovalLog>();
}
