using System;
using System.Collections.Generic;

namespace SiTagAPI_1._0.Models;

public partial class User
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string Password { get; set; } = null!;

    public string Cellphone { get; set; } = null!;

    public virtual ICollection<Farm> Farms { get; set; } = new List<Farm>();

    public virtual ICollection<Finance> Finances { get; set; } = new List<Finance>();
}
