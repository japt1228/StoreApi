using System;
using System.Collections.Generic;

namespace StoreApi.Models;

public partial class Sale
{
    public int Id { get; set; }

    public string Code { get; set; } = null!;

    public decimal Total { get; set; }

    public DateTime SaleDate { get; set; }

    public int UserId { get; set; }

    public int ClientId { get; set; }

    public virtual Client? Client { get; set; }

    public virtual User? User { get; set; }

    public virtual ICollection<Product> Products { get; set; } = new List<Product>();
}

