using System;
using System.Collections.Generic;

namespace SiTagAPI_1._0.Models;

public partial class Finance
{
    public int Id { get; set; }

    public int UserId { get; set; }

    public double Amount { get; set; }

    public DateOnly Date { get; set; }

    public string Description { get; set; } = null!;

    public byte Type { get; set; }

    public string Sender { get; set; } = null!;

    public string? Address { get; set; }

    public virtual User User { get; set; } = null!;
}
