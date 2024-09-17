using System;
using System.Collections.Generic;

namespace WebAPI.Models;

public partial class Sponsorship
{
    public long Id { get; set; }

    public string SponsorName { get; set; } = null!;

    public decimal Amount { get; set; }

    public string Description { get; set; } = null!;

    public DateTime DateTime { get; set; }

    public long CompetitorId { get; set; }

    public long CurrencyId { get; set; }

    public virtual Competitor Competitor { get; set; } = null!;

    public virtual Currency Currency { get; set; } = null!;
}
