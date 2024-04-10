using FluentValidation;
using Iridium.Application.CQRS.Workspaces.Commands;
using Iridium.Infrastructure.Constants;

namespace Iridium.Application.CQRS.Workspaces.Validators;

public class InsertWorkspaceCommandValidator : AbstractValidator<InsertWorkspaceCommand>
{
    public InsertWorkspaceCommandValidator()
    {
        RuleFor(v => v.Name)
            .MaximumLength(ConfigurationConstants.MaxWorkspaceLength)
            .NotEmpty();
    }
}
