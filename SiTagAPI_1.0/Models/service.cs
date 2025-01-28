﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace SiTagAPI_1._0.Models;

public partial class service
{
    [Key]
    public int id { get; set; }

    public int animalId { get; set; }

    [Required]
    [StringLength(45)]
    [Unicode(false)]
    public string type { get; set; }

    [Required]
    [Column(TypeName = "text")]
    public string description { get; set; }

    [Column(TypeName = "date")]
    public DateTime date { get; set; }

    [ForeignKey("animalId")]
    [InverseProperty("service")]
    public virtual animal animal { get; set; }
}