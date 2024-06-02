namespace Iridium.Domain.Entities;

public class AuditLog
{
    public int Id { get; set; }

    public string EntityId { get; set; }

    public string EntityName { get; set; }

    public string? OldValue { get; set; }

    public string? NewValue { get; set; }

    public short Type { get; set; }

    public DateTime Timestamp { get; set; }

    public long UserId { get; set; }
}