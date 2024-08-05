using System;
using System.Collections.Generic;

namespace DataAccess.Models;

public partial class OrderHistory
{
    public int OrderHistoryId { get; set; }

    public int? OrderId { get; set; }

    public string Status { get; set; }

    public DateTime? StatusChangeDate { get; set; }

    public string Notes { get; set; }

    public virtual Order Order { get; set; }
}
