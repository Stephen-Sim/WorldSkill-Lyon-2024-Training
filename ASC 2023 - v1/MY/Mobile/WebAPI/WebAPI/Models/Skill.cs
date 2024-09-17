using System;
using System.Collections.Generic;

namespace WebAPI.Models;

public partial class Skill
{
    public long Id { get; set; }

    public string Name { get; set; } = null!;

    public string Description { get; set; } = null!;

    public virtual ICollection<Competitor> Competitors { get; set; } = new List<Competitor>();
}
