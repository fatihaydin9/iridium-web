using System.ComponentModel.DataAnnotations.Schema;
using Iridium.Domain.Common;

namespace Iridium.Domain.Entities;

public class NoteKeyword : BaseDomainEntity
{
    // Properties
    public long NoteId { get; set; }
    public long KeywordId { get; set; }

    // Relationships
    [ForeignKey("NoteId")]
    public virtual Note Note { get; set; }
    
    [ForeignKey("KeywordId")]
    public virtual Keyword Keyword { get; set; }
}