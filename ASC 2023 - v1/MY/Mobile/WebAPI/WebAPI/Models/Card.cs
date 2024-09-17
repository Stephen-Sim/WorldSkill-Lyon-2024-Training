using System;
using System.Collections.Generic;

namespace WebAPI.Models;

public partial class Card
{
    public int Id { get; set; }

    public string? CardNo { get; set; }

    public decimal? Balance { get; set; }

    public string? Cvv { get; set; }
}
