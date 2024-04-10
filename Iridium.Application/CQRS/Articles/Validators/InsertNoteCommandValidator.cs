using FluentValidation;
using Iridium.Application.CQRS.Articles.Commands;
using Iridium.Infrastructure.Constants;

namespace Iridium.Application.CQRS.Articles.Validators;

public class InsertArticleCommandValidator : AbstractValidator<InsertArticleCommand>
{
    public InsertArticleCommandValidator()
    {
        RuleFor(v => v.WorkspaceId)
            .NotEqual(0);

        RuleFor(v => v.Title)
            .MinimumLength(ConfigurationConstants.MinNoteTitleLength)
            .MaximumLength(ConfigurationConstants.MaxNoteTitleLength)
            .NotEmpty();
        
        RuleFor(v => v.Content)
            .MinimumLength(ConfigurationConstants.MinNoteContentLength)
            .MaximumLength(ConfigurationConstants.MaxNoteContentLength)
            .NotEmpty();
        
        RuleFor(v => v.Content)
            .MinimumLength(ConfigurationConstants.MinNoteSummaryLength)
            .MaximumLength(ConfigurationConstants.MaxNoteSummaryLength)
            .NotEmpty();

    }
}