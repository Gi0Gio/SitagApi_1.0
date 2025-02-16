using System;
using System.Collections.Generic;

namespace SiTagAPI_1._0.Models;

public partial class MedicalService
{
    public int Id { get; set; }

    public int AnimalId { get; set; }

    public string Drug { get; set; } = null!;

    public DateOnly Date { get; set; }

    public string Reason { get; set; } = null!;

    public virtual Animal Animal { get; set; } = null!;
}
