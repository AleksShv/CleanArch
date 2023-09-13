using System.Text.RegularExpressions;

using FluentValidation;

namespace CleanArch.UseCases.Purchasing.Vendors.AddVendor;

internal partial class AddVendorCommandValidator : AbstractValidator<AddVendorCommand>
{
    public AddVendorCommandValidator()
    {
        RuleFor(x => x.Name)
            .MinimumLength(4)
            .MaximumLength(64);

        RuleFor(x => x.OGRN)
            .Length(13)
            .Matches(OnlyNumbers())
                .WithErrorCode("InvalidOgrnFormat")
                .WithMessage("Invalid OGRN format");

        RuleFor(x => x.INN)
            .Length(12)
            .Matches(OnlyNumbers())
                .WithErrorCode("InvalidInnFormat")
                .WithMessage("Invalid INN format");

        RuleFor(x => x.KPP)
            .Length(9)
            .Matches(OnlyNumbers())
                .WithErrorCode("InvalidKppFormat")
                .WithMessage("Invalid KPP format"); ;
    }

    [GeneratedRegex("^[0-9]+$")]
    public partial Regex OnlyNumbers();
}