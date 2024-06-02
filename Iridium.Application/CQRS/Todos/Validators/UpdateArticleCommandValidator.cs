using FluentValidation;
using Iridium.Application.CQRS.Todos.Commands;
using Iridium.Core.Constants;

namespace Iridium.Application.CQRS.Todos.Validators;

public class UpdateTodoCommandValidator : AbstractValidator<UpdateTodoCommand>
{
    public UpdateTodoCommandValidator()
    {
        RuleFor(v => v.Id)
            .NotNull()
            
            .NotEqual(0);
        RuleFor(v => v.Content)
            .MinimumLength(ConfigurationConstants.MinTodoContentLength)
            .MaximumLength(ConfigurationConstants.MaxTodoContentLength)
            .NotEmpty();
    }
}
