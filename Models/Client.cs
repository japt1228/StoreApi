using System;
using System.Collections.Generic;

namespace StoreApi.Models;

public partial class Client
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string Document { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string Phone { get; set; } = null!;

    public string Address { get; set; } = null!;

    public DateTime Dob { get; set; }

    public virtual ICollection<Sale> Sales { get; set; } = new List<Sale>();
}
