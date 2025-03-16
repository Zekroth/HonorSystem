using System;
using System.Collections.Generic;

namespace HonorSystem.sakila;

public partial class Honorentry
{
    public int IdHonorEntry { get; set; }

    public DateTime EntryDate { get; set; }

    /// <summary>
    /// defaults to the related type default number.
    /// </summary>
    public int AssignedPoints { get; set; }

    public string? Description { get; set; }

    public DateTime? ExpirationDate { get; set; }

    public int? HonorEntryTypeId { get; set; }

    public int PlayerId { get; set; }

    public virtual Honorentrytype? HonorEntryType { get; set; }

    public virtual ICollection<Leftiteminguildstorage> Leftiteminguildstorages { get; set; } = new List<Leftiteminguildstorage>();

    public virtual Member? Player { get; set; }
}
