using FluentValidation;
using Iridium.Application.CQRS.Workspaces.Commands;
using Iridium.Infrastructure.Constants;

namespace Iridium.Application.CQRS.Workspaces.Validators;

public class UpdateWorkspaceCommandValidator : AbstractValidator<UpdateWorkspaceCommand>
{
    public UpdateWorkspaceCommandValidator()
    {
        RuleFor(v => v.Id)
            .NotNull()
            .NotEqual(0);

        RuleFor(v => v.Name)
            .MaximumLength(ConfigurationConstants.MaxWorkspaceLength)
            .NotEmpty();
    }
}
