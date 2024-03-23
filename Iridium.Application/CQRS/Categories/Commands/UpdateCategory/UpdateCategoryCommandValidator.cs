using FluentValidation;
using Iridium.Infrastructure.Constants;

namespace Iridium.Application.CQRS.Categories.Commands.UpdateCategory;

public class UpdateCategoryCommandValidator : AbstractValidator<UpdateCategoryCommand>
{
    public UpdateCategoryCommandValidator()
    {
        RuleFor(v => v.Id)
            .NotNull()
            .NotEqual(0);

        RuleFor(v => v.Name)
            .MaximumLength(ConfigurationConstants.MaxCategoryLength)
            .NotEmpty();
    }
}
