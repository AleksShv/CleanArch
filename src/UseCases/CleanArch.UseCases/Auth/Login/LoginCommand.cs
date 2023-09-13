using MediatR;

namespace CleanArch.UseCases.Auth.Login;

public record LoginCommand(
    string Email,
    string Password) : IRequest<LoginResultDto>;