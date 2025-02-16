using System;
using System.Collections.Generic;

namespace SiTagAPI_1._0.Models;

public partial class Activity
{
    public int Id { get; set; }

    public int AnimalId { get; set; }

    public string Type { get; set; } = null!;

    public string Description { get; set; } = null!;

    public DateOnly Date { get; set; }

    public virtual Animal Animal { get; set; } = null!;
}
