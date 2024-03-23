using FluentValidation;
using Iridium.Infrastructure.Constants;

namespace Iridium.Application.CQRS.Categories.Commands.InsertCategory;

public class InsertCategoryCommandValidator : AbstractValidator<InsertCategoryCommand>
{
    public InsertCategoryCommandValidator()
    {
        RuleFor(v => v.Name)
            .MaximumLength(ConfigurationConstants.MaxCategoryLength)
            .NotEmpty();
    }
}
