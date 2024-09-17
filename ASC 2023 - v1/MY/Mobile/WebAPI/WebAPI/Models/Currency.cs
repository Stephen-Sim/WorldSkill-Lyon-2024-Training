using System;
using System.Collections.Generic;

namespace WebAPI.Models;

public partial class Currency
{
    public long Id { get; set; }

    public string Name { get; set; } = null!;

    public decimal Rate { get; set; }

    public virtual ICollection<Country> Countries { get; set; } = new List<Country>();

    public virtual ICollection<Sponsorship> Sponsorships { get; set; } = new List<Sponsorship>();
}
