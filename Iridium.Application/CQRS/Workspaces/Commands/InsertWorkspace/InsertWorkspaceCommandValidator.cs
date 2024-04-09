using FluentValidation;
using Iridium.Infrastructure.Constants;

namespace Iridium.Application.CQRS.Categories.Commands.InsertWorkspace;

public class InsertWorkspaceCommandValidator : AbstractValidator<InsertWorkspaceCommand>
{
    public InsertWorkspaceCommandValidator()
    {
        RuleFor(v => v.Name)
            .MaximumLength(ConfigurationConstants.MaxWorkspaceLength)
            .NotEmpty();
    }
}
