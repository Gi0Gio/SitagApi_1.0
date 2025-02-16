using System;
using System.Collections.Generic;

namespace SiTagAPI_1._0.Models;

public partial class FarmDivision
{
    public int Id { get; set; }

    public int FarmId { get; set; }

    public string Name { get; set; } = null!;

    public byte State { get; set; }

    public virtual ICollection<AnimalDatum> AnimalData { get; set; } = new List<AnimalDatum>();

    public virtual Farm Farm { get; set; } = null!;
}
