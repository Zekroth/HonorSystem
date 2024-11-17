
using HonorSystem.sakila;

namespace HonorSystem.ViewModels;



public class BulkInsertViewModel
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

    public List<int> PlayerId { get; set; } = new List<int>();

    public virtual Honorentrytype? HonorEntryType { get; set; } = null;

    public virtual ICollection<Leftiteminguildstorage> Leftiteminguildstorages { get; set; } = new List<Leftiteminguildstorage>();

    public virtual ICollection<Member> Players { get; set; } = new List<Member>();

    public Honorentry GetHonorentry { 
        get { 
            return new Honorentry () { 
                HonorEntryTypeId = HonorEntryTypeId,
                IdHonorEntry = IdHonorEntry,
                AssignedPoints = AssignedPoints,
                Description = Description,
                ExpirationDate = ExpirationDate,
                EntryDate = EntryDate,
            };
        }
    }
}