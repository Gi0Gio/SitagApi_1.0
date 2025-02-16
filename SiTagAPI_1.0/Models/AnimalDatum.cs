using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace SiTagAPI_1._0.Models;

public partial class AnimalDatum
{
    public int Id { get; set; }

    public int AnimalId { get; set; }

    public string Weight { get; set; } = null!;

    public DateTime EntryDate { get; set; }

    public int DivisionId { get; set; }

    public int State { get; set; }

    [JsonIgnore]
    public virtual Animal Animal { get; set; } = null!;
    [JsonIgnore]
    public virtual FarmDivision Division { get; set; } = null!;
}
