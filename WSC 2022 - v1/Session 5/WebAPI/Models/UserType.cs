using System;
using System.Collections.Generic;

namespace Asp.net_core_web_api.Models;

public partial class UserType
{
    public long Id { get; set; }

    public Guid Guid { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<User> Users { get; set; } = new List<User>();
}
