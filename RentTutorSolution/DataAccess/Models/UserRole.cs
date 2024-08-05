using System;
using System.Collections.Generic;

namespace DataAccess.Models;

public partial class UserRole
{
    public int UserRoleId { get; set; }

    public int? UserId { get; set; }

    public string Role { get; set; }

    public DateTime? CreatedAt { get; set; }

    public virtual User User { get; set; }
}
