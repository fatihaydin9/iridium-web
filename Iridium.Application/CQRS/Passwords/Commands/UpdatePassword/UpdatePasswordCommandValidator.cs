using FluentValidation;
using Iridium.Infrastructure.Constants;

namespace Iridium.Application.CQRS.Passwords.Commands.UpdatePassword;

public class UpdatePasswordCommandValidator : AbstractValidator<UpdatePasswordCommand>
{
    public UpdatePasswordCommandValidator()
    {
        RuleFor(v => v.Id)
            .NotNull()
            .NotEqual(0);

        RuleFor(v => v.CategoryId)
            .NotNull()
            .NotEqual(0);

        RuleFor(v => v.Username)
            .MaximumLength(ConfigurationConstants.MAX_USERNAME_LENGTH)
            .NotEmpty();
    }
}
