using System;
using System.Collections.Generic;

namespace HonorSystem.sakila;

public partial class Item
{
    public int IdItem { get; set; }

    public string? ItemName { get; set; }

    public int IdBoss { get; set; }

    public virtual Boss? IdBossNavigation { get; set; } = null;

    public virtual ICollection<Itemrequest> Itemrequests { get; set; } = new List<Itemrequest>();

    public virtual ICollection<Leftiteminguildstorage> Leftiteminguildstorages { get; set; } = new List<Leftiteminguildstorage>();
}
