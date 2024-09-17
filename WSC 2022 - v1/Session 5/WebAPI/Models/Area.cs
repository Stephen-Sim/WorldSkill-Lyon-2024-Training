using System;
using System.Collections.Generic;

namespace Asp.net_core_web_api.Models;

public partial class Area
{
    public long Id { get; set; }

    public Guid Guid { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<Attraction> Attractions { get; set; } = new List<Attraction>();

    public virtual ICollection<Item> Items { get; set; } = new List<Item>();
}
