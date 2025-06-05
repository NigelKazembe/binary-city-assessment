using System;
using System.Collections.Generic;

namespace bcity_assessment;

public partial class Contact
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public string? Surname { get; set; }

    public string? Email { get; set; }

    public virtual ICollection<Client> Clients { get; set; } = new List<Client>();
}
