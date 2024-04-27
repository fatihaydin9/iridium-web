﻿using Iridium.Domain.Common;
using Iridium.Domain.Entities;

namespace Iridium.Domain.Events;

public class WorkspaceDeletedEvent : BaseEvent
{
    public WorkspaceDeletedEvent(Workspace workspace)
    {
        Workspace = workspace;
    }

    public Workspace Workspace { get; }
}