using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace HonorSystem.sakila;

public partial class Itemrequest
{
    [Key]
    public int IdItemRequest { get; set; }

    public sbyte IsSupplied { get; set; }

    public int ItemId { get; set; }

    public int PlayerId { get; set; }

    public virtual Item? Item { get; set; } = null;

    public virtual Member? Player { get; set; } = null;
}
