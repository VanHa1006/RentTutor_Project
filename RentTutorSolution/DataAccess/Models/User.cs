using System;
using System.Collections.Generic;

namespace DataAccess.Models;

public partial class User
{
    public int UserId { get; set; }

    public string FirstName { get; set; }

    public string LastName { get; set; }

    public string Email { get; set; }

    public string PhoneNumber { get; set; }

    public string PasswordHash { get; set; }

    public string Role { get; set; }

    public string Specialization { get; set; }

    public string Bio { get; set; }

    public decimal? Rating { get; set; }

    public decimal? HourlyRate { get; set; }

    public bool? IsActive { get; set; }

    public DateTime? CreatedAt { get; set; }

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();

    public virtual ICollection<TutorSubject> TutorSubjects { get; set; } = new List<TutorSubject>();

    public virtual ICollection<UserRole> UserRoles { get; set; } = new List<UserRole>();
}
