using Iridium.Domain.Common;

namespace Iridium.Domain.Entities;

public class Todo : BaseDomainEntity
{
    public string Content { get; set; }
    public bool IsCompleted { get; set; }
}