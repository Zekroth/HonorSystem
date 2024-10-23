using System;
using System.Collections.Generic;

namespace HonorSystem.sakila;

public partial class Boss
{
    public int IdBoss { get; set; }

    public string? BossName { get; set; }

    public sbyte IsField { get; set; }

    public sbyte IsArch { get; set; }

    public sbyte IsGuild { get; set; }

    public virtual ICollection<Item> Items { get; set; } = new List<Item>();
}
