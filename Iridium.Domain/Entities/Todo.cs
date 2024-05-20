using Iridium.Domain.Common;
using System.ComponentModel.DataAnnotations.Schema;

namespace Iridium.Domain.Entities;

public class Todo : BaseDomainEntity
{
    public string Content { get; set; }
    public bool IsCompleted { get; set; }
}
