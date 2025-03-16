using System;
using System.Collections.Generic;

namespace HonorSystem.sakila;

public partial class Member
{
    public int IdMembers { get; set; }

    public string Name { get; set; } = null!;

    public string CharacterName { get; set; } = null!;

    public string? SecondaryCharacterName { get; set; }

    public DateTime? JoinDate { get; set; }

    public sbyte IsActive { get; set; }

    public sbyte IsStillInGuild { get; set; }

    public virtual ICollection<Droppeditemsrequest> Droppeditemsrequests { get; set; } = new List<Droppeditemsrequest>();

    public virtual ICollection<Honorentry> Honorentries { get; set; } = new List<Honorentry>();

    public virtual ICollection<Itemrequest> Itemrequests { get; set; } = new List<Itemrequest>();

    public virtual ICollection<Leftiteminguildstorage> Leftiteminguildstorages { get; set; } = new List<Leftiteminguildstorage>();
}
