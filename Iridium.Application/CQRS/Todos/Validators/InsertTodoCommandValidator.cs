using FluentValidation;
using Iridium.Application.CQRS.Todos.Commands;
using Iridium.Infrastructure.Constants;

namespace Iridium.Application.CQRS.Todos.Validators;

public class InsertTodoCommandValidator : AbstractValidator<InsertTodoCommand>
{
    public InsertTodoCommandValidator()
    {
        RuleFor(v => v.Content)
            .MinimumLength(ConfigurationConstants.MinTodoContentLength)
            .MaximumLength(ConfigurationConstants.MaxTodoContentLength)
            .NotEmpty();
    }
}