using System;
using System.Collections.Generic;

namespace WebAPI.Models;

public partial class Country
{
    public long Id { get; set; }

    public string Name { get; set; } = null!;

    public string Code { get; set; } = null!;

    public long CurrencyId { get; set; }

    public virtual ICollection<Competitor> Competitors { get; set; } = new List<Competitor>();

    public virtual Currency Currency { get; set; } = null!;
}
