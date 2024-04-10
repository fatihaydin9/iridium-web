using Iridium.Domain.Common;
using Iridium.Domain.Entities;

namespace Iridium.Domain.Events;

public class WorkspaceUpdatedEvent : BaseEvent
{
    public WorkspaceUpdatedEvent(Workspace workspace)
    {
        Workspace = workspace;
    }

    public Workspace Workspace { get; }
}