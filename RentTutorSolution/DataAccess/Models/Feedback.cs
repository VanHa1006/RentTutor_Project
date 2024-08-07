using System;
using System.Collections.Generic;

namespace DataAccess.Models;

public partial class Feedback
{
    public int FeedbackId { get; set; }

    public int StudentId { get; set; }

    public int CourseId { get; set; }

    public int Rating { get; set; }

    public string Comment { get; set; }

    public DateTime? CreatedAt { get; set; }

    public virtual Course Course { get; set; }

    public virtual Student Student { get; set; }
}
