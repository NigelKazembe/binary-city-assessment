using System;
using System.Collections.Generic;

namespace bcity_assessment;

public partial class Client
{
    public int Id { get; set; }

    public string? ClientCode { get; set; }

    public virtual ICollection<Contact> Contacts { get; set; } = new List<Contact>();
}
