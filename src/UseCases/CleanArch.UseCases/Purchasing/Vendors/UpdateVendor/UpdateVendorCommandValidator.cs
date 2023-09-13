using CleanArch.DataAccess.Contracts;
using CleanArch.UseCases.Common.Utils;
using CleanArch.UseCases.Purchasing.Vendors.AddVendor;
using FluentValidation;

namespace CleanArch.UseCases.Purchasing.Vendors.UpdateVendor;

internal class UpdateVendorCommandValidator : AbstractValidator<UpdateVendorCommand>
{
    public UpdateVendorCommandValidator(IValidator<AddVendorCommand> validator, IApplicationDbContext context)
    {
        RuleFor(x => x.Id)
            .MustAsync(context.Vendors.ContainsWithIdAsync);

        Include(validator);
    }
}
