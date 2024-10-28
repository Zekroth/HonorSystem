using System;
using System.Collections.Generic;

namespace HonorSystem.sakila;

public partial class Honorentrytype
{
    public int IdHonorEntryType { get; set; }

    public string Type { get; set; } = null!;

    public int DefaultPoints { get; set; }

    public string? Description { get; set; }

    public int Expiration { get; set; }

    public virtual ICollection<Honorentry> Honorentries { get; set; } = new List<Honorentry>();
}
