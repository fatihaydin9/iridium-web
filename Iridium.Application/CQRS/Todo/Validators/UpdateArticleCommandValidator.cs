using FluentValidation;
using Iridium.Application.CQRS.Articles.Commands;
using Iridium.Infrastructure.Constants;

namespace Iridium.Application.CQRS.Articles.Validators;

public class UpdateArticleCommandValidator : AbstractValidator<UpdateTodoCommand>
{
    public UpdateArticleCommandValidator()
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
