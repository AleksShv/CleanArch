using MediatR;

using CleanArch.Entities;
using CleanArch.Infrastructure.Contracts.UserProvider;
using CleanArch.UseCases.Common.Requests;

namespace CleanArch.UseCases.Common.Behaviours;

internal class RoleBasedRequestBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRoleBasedRequest
{
    private readonly ICurrentUserProvider _userProvider;

    public RoleBasedRequestBehavior(ICurrentUserProvider userProvider)
    {
        _userProvider = userProvider;
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        var currentRole = _userProvider.GetRoles<UserRole>();
        if (!currentRole.HasFlag(request.AccessRole))
        {
            throw new UnauthorizedAccessException();
        }

        return await next();
    }
}
