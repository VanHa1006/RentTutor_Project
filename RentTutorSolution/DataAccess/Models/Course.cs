using System;
using System.Collections.Generic;

namespace DataAccess.Models;

public partial class Course
{
    public int CourseId { get; set; }

    public string CourseName { get; set; }

    public string Description { get; set; }

    public decimal Price { get; set; }

    public int CategoryId { get; set; }

    public int TutorId { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public virtual Category Category { get; set; }

    public virtual ICollection<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();

    public virtual User Tutor { get; set; }
}
