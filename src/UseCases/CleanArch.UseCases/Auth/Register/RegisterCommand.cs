using MediatR;

using CleanArch.UseCases.Common.Requests;

namespace CleanArch.UseCases.Auth.Register;

public record RegisterCommand(
    string Email,
    string Password,
    string ConfirmPassword,
    string FirstName,
    string? LastName,
    string? PhoneNumber) : IRequest<Guid>, IValidatableRequest;
