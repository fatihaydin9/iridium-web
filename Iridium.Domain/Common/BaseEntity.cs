﻿namespace Iridium.Domain.Common;

public class BaseEntity
{
    public long Id { get; set; }

    public Guid GuidId { get; init; }

    public DateTime CreatedDate { get; set; }

    public long? CreatedBy { get; set; }

    public DateTime? ModifiedDate { get; set; }

    public long? ModifiedBy { get; set; }

    public bool? Deleted { get; set; }
}
