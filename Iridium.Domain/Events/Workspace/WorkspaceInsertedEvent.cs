using Iridium.Domain.Common;
using Iridium.Domain.Entities;

namespace Iridium.Domain.Events;

public class WorkspaceInsertedEvent : BaseEvent
{
    public WorkspaceInsertedEvent(Workspace workspace)
    {
        Workspace = workspace;
    }

    public Workspace Workspace { get; }
}