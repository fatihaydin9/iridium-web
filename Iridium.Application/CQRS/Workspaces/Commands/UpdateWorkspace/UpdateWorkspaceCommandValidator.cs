using FluentValidation;
using Iridium.Infrastructure.Constants;

namespace Iridium.Application.CQRS.Categories.Commands.UpdateWorkspace;

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
