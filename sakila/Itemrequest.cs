﻿using System;
using System.Collections.Generic;

namespace HonorSystem.sakila;

public partial class Itemrequest
{
    public int IdItemRequest { get; set; }

    public sbyte IsSupplied { get; set; }

    public int ItemId { get; set; }

    public int PlayerId { get; set; }

    public virtual Item? Item { get; set; }

    public virtual Member? Player { get; set; }
}
