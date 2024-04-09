using System.ComponentModel.DataAnnotations.Schema;
using Iridium.Domain.Common;

namespace Iridium.Domain.Entities;

public class Concept : BaseDomainEntity
{
    // Properties
    public long Id { get; set; }
    public long NoteId { get; set; }
    public string Name { get; set; }
    

    // Relationships
    [ForeignKey("NoteId")]
    public virtual Note Note { get; set; }
}
