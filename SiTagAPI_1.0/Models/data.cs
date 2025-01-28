﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;

namespace SiTagAPI_1._0.Models;

public partial class data
{
    [Key]
    public int id { get; set; }

    public int animal_id { get; set; }

    [Required]
    [StringLength(45)]
    [Unicode(false)]
    public string weight { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime entryDate { get; set; }

    public int divisionId { get; set; }

    public int state { get; set; }

    [ForeignKey("animal_id")]
    [InverseProperty("data")]
    [JsonIgnore]
    public virtual animal animal { get; set; }

    [ForeignKey("divisionId")]
    [InverseProperty("data")]
    [JsonIgnore]
    public virtual division division { get; set; }
}