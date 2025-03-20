using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace HonorSystem.sakila;

public partial class Droppeditemsrequest
{
    public int IdDroppedItemsRequests { get; set; }

    public DateTime RequestDate { get; set; }

    public int IdMember { get; set; }

    public int IdLeftItemInGuildStorage { get; set; }

    public string? Reason { get; set; }

    public virtual Leftiteminguildstorage? IdLeftItemInGuildStorageNavigation { get; set; }

    public virtual Member? IdMemberNavigation { get; set; }
}
