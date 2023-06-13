using System;
using System.Collections.Generic;

namespace StoreApi.Models;

public partial class Product
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string Code { get; set; } = null!;

    public string Description { get; set; } = null!;

    public int Stock { get; set; }

    public decimal PurchasePrice { get; set; }

    public decimal SalePrice { get; set; }

    public int ProviderId { get; set; }

    public int CategoryId { get; set; }

    public virtual Category? Category { get; set; }

    public virtual Provider? Provider { get; set; }

    public virtual ICollection<Sale> Sales { get; set; } = new List<Sale>();
}


