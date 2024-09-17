using System;
using System.Collections.Generic;

namespace Asp.net_core_web_api.Models;

public partial class Amenity
{
    public long Id { get; set; }

    public Guid Guid { get; set; }

    public string Name { get; set; } = null!;

    public string? IconName { get; set; }

    public virtual ICollection<ItemAmenity> ItemAmenities { get; set; } = new List<ItemAmenity>();
}
