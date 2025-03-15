using System;
using System.Collections.Generic;

namespace HonorSystem.sakila;

public partial class ItemRequestedPerBoss
{
    public long TotaleRichieste { get; set; }

    public string? ItemName { get; set; }

    public string? BossName { get; set; }
}
