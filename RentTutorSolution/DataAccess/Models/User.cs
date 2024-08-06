using System;
using System.Collections.Generic;

namespace DataAccess.Models;

public partial class User
{
    public int UserId { get; set; }

    public string Username { get; set; }

    public string Email { get; set; }

    public string PasswordHash { get; set; }

    public string Role { get; set; }

    public string Status { get; set; }

    public string Qualifications { get; set; }

    public string Experience { get; set; }

    public string Specialization { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public virtual ICollection<Course> Courses { get; set; } = new List<Course>();

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();

    public virtual ICollection<UserApprovalLog> UserApprovalLogAdmins { get; set; } = new List<UserApprovalLog>();

    public virtual ICollection<UserApprovalLog> UserApprovalLogUsers { get; set; } = new List<UserApprovalLog>();
}
