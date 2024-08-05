using System;
using System.Collections.Generic;

namespace DataAccess.Models;

public partial class Order
{
    public int OrderId { get; set; }

    public int? StudentId { get; set; }

    public int? TutorSubjectId { get; set; }

    public string Status { get; set; }

    public DateTime? OrderDate { get; set; }

    public decimal? Amount { get; set; }

    public string PaymentStatus { get; set; }

    public DateTime? CreatedAt { get; set; }

    public virtual ICollection<OrderHistory> OrderHistories { get; set; } = new List<OrderHistory>();

    public virtual User Student { get; set; }

    public virtual TutorSubject TutorSubject { get; set; }
}
