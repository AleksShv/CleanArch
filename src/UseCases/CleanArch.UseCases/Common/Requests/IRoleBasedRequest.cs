using MediatR;

using CleanArch.Entities;

namespace CleanArch.UseCases.Common.Requests;

internal interface IRoleBasedRequest : IBaseRequest
{
    UserRole AccessRole { get; }
}
