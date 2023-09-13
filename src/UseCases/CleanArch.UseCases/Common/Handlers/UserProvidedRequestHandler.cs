using MediatR;

using CleanArch.Entities;
using CleanArch.Infrastructure.Contracts.UserProvider;

namespace CleanArch.UseCases.Common.Handlers;

internal class UserProvidedRequestHandler<TRequest, TResponse> : IRequestHandler<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
{
    public UserProvidedRequestHandler(ICurrentUserProvider userProvider)
    {
        UserId = userProvider.GetUserId<Guid>();
        Role = userProvider.GetRoles<UserRole>();
    }

    public Guid UserId { get; }
    public UserRole Role { get; }

    public virtual Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}

internal class UserProvidedRequestHandler<TRequest> : IRequestHandler<TRequest>
    where TRequest : IRequest
{
    public UserProvidedRequestHandler(ICurrentUserProvider userProvider)
    {
        UserId = userProvider.GetUserId<Guid>();
        Role = userProvider.GetRoles<UserRole>();
    }

    public Guid UserId { get; }
    public UserRole Role { get; }

    public virtual Task Handle(TRequest request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
