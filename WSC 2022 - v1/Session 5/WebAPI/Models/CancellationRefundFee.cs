using System;
using System.Collections.Generic;

namespace Asp.net_core_web_api.Models;

public partial class CancellationRefundFee
{
    public long Id { get; set; }

    public Guid Guid { get; set; }

    public long CancellationPolicyId { get; set; }

    public int DaysLeft { get; set; }

    public decimal PenaltyPercentage { get; set; }

    public virtual CancellationPolicy CancellationPolicy { get; set; } = null!;
}
