using System;
using System.Collections.Generic;

namespace Asp.net_core_web_api.Models;

public partial class Score
{
    public long Id { get; set; }

    public Guid Guid { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<ItemScore> ItemScores { get; set; } = new List<ItemScore>();
}
