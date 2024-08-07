using System;
using System.Collections.Generic;

namespace DataAccess.Models;

public partial class Student
{
    public int StudentId { get; set; }

    public virtual ICollection<Feedback> Feedbacks { get; set; } = new List<Feedback>();

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();

    public virtual User StudentNavigation { get; set; }
}
