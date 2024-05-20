using FluentValidation;
using Iridium.Application.CQRS.Articles.Commands;
using Iridium.Infrastructure.Constants;

namespace Iridium.Application.CQRS.Articles.Validators;

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