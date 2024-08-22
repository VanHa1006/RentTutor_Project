using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace DataAccess.Models;

public partial class User
{
    public int UserId { get; set; }

    public string Username { get; set; }

    public string Email { get; set; }

    public string PasswordHash { get; set; }

    public string Role { get; set; }

    public string Status { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public string Phone { get; set; }

    public string Address { get; set; }

    public DateOnly? Birthday { get; set; }

    public string FullName { get; set; }

    public virtual Student Student { get; set; }

    public virtual Tutor Tutor { get; set; }

    public virtual ICollection<UserApprovalLog> UserApprovalLogs { get; set; } = new List<UserApprovalLog>();
}
