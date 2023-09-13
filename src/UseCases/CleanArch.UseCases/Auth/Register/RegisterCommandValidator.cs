using FluentValidation;

using CleanArch.DataAccess.Contracts;
using CleanArch.Utils;
using CleanArch.UseCases.Common.Utils;

namespace CleanArch.UseCases.Auth.Register;

internal class RegisterCommandValidator : AbstractValidator<RegisterCommand>
{
    public RegisterCommandValidator(IApplicationDbContext context)
    {
        RuleFor(x => x.Email)
            .EmailAddress()
            .MaximumLength(30)
            .MustAsync(async (e, ct) => !await context.Users.ContainsWithAsync(u => u.Email.ToLower(), e.ToLower(), ct))
                .WithErrorCode("UerAlreadyExist")
                .WithMessage("User with the same email already exist");

        RuleFor(x => x.Password)
            .MinimumLength(8);

        RuleFor(x => x.ConfirmPassword)
            .Equal(x => x.Password);

        RuleFor(x => x.PhoneNumber)
            .Matches("^(\\+7|7|8)[0-9]{10}")
                .WithErrorCode("InvalidPhoneNumber")
                .WithMessage("Invalid phone number format")
            .When(x => !x.PhoneNumber.IsNullOrWhiteSpaces());
    }
}
