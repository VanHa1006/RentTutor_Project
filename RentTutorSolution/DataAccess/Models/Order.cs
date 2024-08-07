using System;
using System.Collections.Generic;

namespace DataAccess.Models;

public partial class Order
{
    public int OrderId { get; set; }

    public int StudentId { get; set; }

    public DateTime? OrderDate { get; set; }

    public decimal TotalAmount { get; set; }

    public string Status { get; set; }

    public virtual ICollection<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();

    public virtual Student Student { get; set; }
}
