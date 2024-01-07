using FluentValidation;
using Iridium.Infrastructure.Constants;

namespace Iridium.Application.CQRS.Passwords.Commands.InsertPassword;

public class InsertPasswordCommandValidator : AbstractValidator<InsertPasswordCommand>
{
    public InsertPasswordCommandValidator()
    {
        RuleFor(v => v.CategoryId)
            .NotEqual(0);

    }
}
