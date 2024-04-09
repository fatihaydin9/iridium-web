using FluentValidation;
using Iridium.Application.CQRS.Notes.Commands.UpdateNote;
using Iridium.Infrastructure.Constants;

namespace Iridium.Application.CQRS.Notes.Commands.UpdateNote;

public class UpdateNoteCommandValidator : AbstractValidator<UpdateNoteCommand>
{
    public UpdateNoteCommandValidator()
    {
        RuleFor(v => v.Id)
            .NotNull()
            .NotEqual(0);

        RuleFor(v => v.WorkspaceId)
            .NotNull()
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
