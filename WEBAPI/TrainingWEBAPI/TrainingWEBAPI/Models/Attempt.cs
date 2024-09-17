using System;
using System.Collections.Generic;

namespace TrainingWEBAPI.Models;

public partial class Attempt
{
    public int AttemptId { get; set; }

    public int? QnId { get; set; }

    public string? UserId { get; set; }

    public string? Answer { get; set; }

    public string? DateTime { get; set; }

    public virtual Question? Qn { get; set; }

    public virtual User? User { get; set; }
}
