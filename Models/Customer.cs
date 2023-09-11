using System;
using System.Collections.Generic;

namespace MvcTestCase.Models;

public partial class Customer
{
    public int Id { get; set; }

    public string? Customertitle { get; set; }

    public string? Customernumber { get; set; }

    public virtual ICollection<Purchase> Purchases { get; set; } = new List<Purchase>();

    public virtual ICollection<Sale> Sales { get; set; } = new List<Sale>();
}
