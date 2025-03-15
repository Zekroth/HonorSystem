using System;
using System.Collections.Generic;

namespace HonorSystem.sakila;

public partial class Leftiteminguildstorage
{
    public int Id { get; set; }

    public DateTime DropDate { get; set; }

    public int CurrentPriceInLucent { get; set; }

    public int IdItem { get; set; }

    public int IdHonorEntry { get; set; }

    public DateTime? DistributedDate { get; set; }

    public int? DistributedTo { get; set; }

    public virtual Member? DistributedToNavigation { get; set; }

    public virtual Honorentry IdHonorEntryNavigation { get; set; } = null!;

    public virtual Item IdItemNavigation { get; set; } = null!;
}
