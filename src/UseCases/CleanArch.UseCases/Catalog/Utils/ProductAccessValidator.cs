using FluentValidation;
using FluentValidation.Validators;

using CleanArch.Entities;
using CleanArch.Infrastructure.Contracts.UserProvider;
using CleanArch.UseCases.InternalServices.Contracts;

namespace CleanArch.UseCases.Catalog.Utils;

internal class ProductAccessValidator<T> : AsyncPropertyValidator<T, Guid>
{
    private readonly ICatalogAccessService _accessService;
    private readonly ICurrentUserProvider _userProvider;

    public ProductAccessValidator(ICatalogAccessService accessService, ICurrentUserProvider userProvider)
    {
        _accessService = accessService;
        _userProvider = userProvider;
    }

    public override string Name => "ProductAccessValidator";

    public override async Task<bool> IsValidAsync(ValidationContext<T> context, Guid value, CancellationToken cancellation)
        => await _accessService.CheckUserProductAccessAsync(value, _userProvider.GetUserId<Guid>(), _userProvider.GetRoles<UserRole>(), cancellation);

    protected override string GetDefaultMessageTemplate(string errorCode)
        => "You aren't owner of the product"; 
}
