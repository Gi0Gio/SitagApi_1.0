using System;
using System.Collections.Generic;

namespace SiTagAPI_1._0.Models;

public partial class Farm
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string Location { get; set; } = null!;

    public int UserId { get; set; }

    public string Description { get; set; } = null!;

    public virtual ICollection<FarmDivision> FarmDivisions { get; set; } = new List<FarmDivision>();

    public virtual User User { get; set; } = null!;
}
