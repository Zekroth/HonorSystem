using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace HonorSystem.sakila;

public partial class Classifica
{
    [Key]
    public string? Name { get; set; }

    public decimal? TotalePunti { get; set; }
}
