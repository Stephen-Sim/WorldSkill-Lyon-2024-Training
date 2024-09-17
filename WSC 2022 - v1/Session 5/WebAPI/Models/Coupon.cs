using System;
using System.Collections.Generic;

namespace Asp.net_core_web_api.Models;

public partial class Coupon
{
    public long Id { get; set; }

    public Guid Guid { get; set; }

    public string CouponCode { get; set; } = null!;

    public decimal DiscountPercent { get; set; }

    public decimal MaximimDiscountAmount { get; set; }

    public virtual ICollection<AddonService> AddonServices { get; set; } = new List<AddonService>();

    public virtual ICollection<Booking> Bookings { get; set; } = new List<Booking>();
}
