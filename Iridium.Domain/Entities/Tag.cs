using System.ComponentModel.DataAnnotations.Schema;

namespace Iridium.Domain.Entities;

public class Tag
{
    public long Id { get; set; }
    public long NoteId { get; set; }
    public string Label { get; set; }
    
    [ForeignKey("NoteId")]
    public virtual Note Note { get; set; }
}