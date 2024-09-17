using System;
using System.Collections.Generic;

namespace WebAPI.Models;

public partial class Competitor
{
    public long Id { get; set; }

    public string Name { get; set; } = null!;

    public decimal RequiredAmount { get; set; }

    public long SkillId { get; set; }

    public long CountryId { get; set; }

    public virtual Country Country { get; set; } = null!;

    public virtual Skill Skill { get; set; } = null!;

    public virtual ICollection<Sponsorship> Sponsorships { get; set; } = new List<Sponsorship>();
}
