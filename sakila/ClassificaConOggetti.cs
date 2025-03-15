using System;
using System.Collections.Generic;

namespace HonorSystem.sakila;

public partial class ClassificaConOggetti
{
    public string Name { get; set; } = null!;

    public decimal? TotalePunti { get; set; }

    public string? ItemName { get; set; }
}
