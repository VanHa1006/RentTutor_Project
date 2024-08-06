using System;
using System.Collections.Generic;

namespace DataAccess.Models;

public partial class UserApprovalLog
{
    public int LogId { get; set; }

    public int UserId { get; set; }

    public int AdminId { get; set; }

    public string Decision { get; set; }

    public DateTime? DecisionDate { get; set; }

    public string Reason { get; set; }

    public virtual User Admin { get; set; }

    public virtual User User { get; set; }
}
