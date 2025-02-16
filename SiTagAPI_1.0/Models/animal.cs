using System;
using System.Collections.Generic;

namespace SiTagAPI_1._0.Models;

public partial class Animal
{
    public int Id { get; set; }

    public string Number { get; set; } = null!;

    public byte Sex { get; set; }

    public string Race { get; set; } = null!;

    public string Specie { get; set; } = null!;

    public string Color { get; set; } = null!;

    public DateOnly Birthdate { get; set; }

    public virtual ICollection<Activity> Activities { get; set; } = new List<Activity>();

    public virtual ICollection<AnimalDatum> AnimalData { get; set; } = new List<AnimalDatum>();

    public virtual ICollection<MedicalService> MedicalServices { get; set; } = new List<MedicalService>();
}
