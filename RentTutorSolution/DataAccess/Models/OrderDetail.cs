using System;
using System.Collections.Generic;

namespace DataAccess.Models;

public partial class OrderDetail
{
    public int OrderDetailId { get; set; }

    public int OrderId { get; set; }

    public int CourseId { get; set; }

    public decimal UnitPrice { get; set; }

    public int Quantity { get; set; }

    public decimal TotalPrice { get; set; }

    public virtual Course Course { get; set; }

    public virtual Order Order { get; set; }
}
