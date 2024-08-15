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

    public string Image { get; set; }

    public string Status { get; set; }

    public string LinkVideo { get; set; }

    public int? Hours { get; set; }

    public decimal DiscountPrice { get; set; }

    public virtual Category Category { get; set; }

    public virtual ICollection<Feedback> Feedbacks { get; set; } = new List<Feedback>();

    public virtual ICollection<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();

    public virtual Tutor Tutor { get; set; }
}
