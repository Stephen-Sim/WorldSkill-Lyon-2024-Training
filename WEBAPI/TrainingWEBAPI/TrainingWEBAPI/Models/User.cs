using System;
using System.Collections.Generic;

namespace TrainingWEBAPI.Models;

public partial class User
{
    public string UserId { get; set; } = null!;

    public string? Name { get; set; }

    public string? Role { get; set; }

    public virtual ICollection<Attempt> Attempts { get; set; } = new List<Attempt>();
}
