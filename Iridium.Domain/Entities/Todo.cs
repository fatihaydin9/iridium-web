using Iridium.Domain.Common;
using System.ComponentModel.DataAnnotations.Schema;
using Ardalis.GuardClauses;

namespace Iridium.Domain.Entities;

public class Todo : BaseDomainEntity
{
    public string Content { get; private set; }
    public bool IsCompleted { get; private set; }

    public Todo(string content, bool isCompleted)
    {
        Content = Guard.Against.NullOrEmpty(content);
        IsCompleted = Guard.Against.Null(isCompleted);
    }

    public void UpdateContent(string content)
    {
        Content = Guard.Against.NullOrEmpty(content);
    }

    public void UpdateIsCompleted(bool isCompleted)
    {
        IsCompleted = Guard.Against.Null(isCompleted);
    }
    
}
