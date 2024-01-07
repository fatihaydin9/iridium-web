using FluentValidation;
using Iridium.Infrastructure.Constants;

namespace Iridium.Application.CQRS.Categories.Commands.UpdateCategory;

public class UpdatePasswordCommandValidator : AbstractValidator<UpdateCategoryCommand>
{
    public UpdatePasswordCommandValidator()
    {
        RuleFor(v => v.Id)
            .NotNull()
            .NotEqual(0);

        RuleFor(v => v.Name)
            .MaximumLength(ConfigurationConstants.MAX_CATEGORY_LENGTH)
            .NotEmpty();
    }
}
