using System;
using System.Collections.Generic;

namespace HonorSystem.sakila;

public partial class ClassificaWithId
{
    public string Name { get; set; } = null!;

    public int IdMember { get; set; }

    public decimal? TotalePunti { get; set; }
}
