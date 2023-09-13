using MediatR;
using Microsoft.EntityFrameworkCore;

using CleanArch.DataAccess.Contracts;
using CleanArch.Entities;
using CleanArch.Utils;
using CleanArch.Infrastructure.Contracts.Authentication;

namespace CleanArch.UseCases.Auth.Login;

internal class LoginCommandHandler : IRequestHandler<LoginCommand, LoginResultDto>
{
    private readonly IApplicationDbContext _context;
    private readonly IPasswordHasher _passwordHasher;
    private readonly IJwtProvider _jwtProvider;
    private readonly IRefreshTokenProvider _refreshTokenProvider;

    public LoginCommandHandler(IApplicationDbContext context, IPasswordHasher passwordHasher, IJwtProvider jwtProvider, IRefreshTokenProvider refreshTokenProvider)
    {
        _context = context;
        _passwordHasher = passwordHasher;
        _jwtProvider = jwtProvider;
        _refreshTokenProvider = refreshTokenProvider;
    }

    public async Task<LoginResultDto> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        var user = await _context.Users
            .Select(u => new
            {
                u.Id,
                u.Email,
                u.FirstName,
                u.Password,
                u.Role
            })
            .FirstOrDefaultAsync(u => u.Email == request.Email, cancellationToken)
            ?? throw new UserNotFoundException(request.Email);

        if (!_passwordHasher.Verify(user.Password, request.Password))
        {
            throw new WrongPasswordException();
        }

        var jwt = _jwtProvider.Generate(new(
            Id: user.Id.ToString(),
            Email: user.Email,
            Name: user.FirstName,
            Roles: EnumHelper.StringValues(user.Role, skipZero: true).JoinWithComma()));

        var refreshToken = _refreshTokenProvider.Generate();

        var token = new RefreshToken
        {
            UserId = user.Id,
            CreatedAt = DateTimeOffset.UtcNow,
            ExpireAt = DateTimeOffset.UtcNow.AddDays(1),
            Token = refreshToken
        };
        await _context.RefreshTokens
            .AddAsync(token, cancellationToken);

        await _context.SaveChangesAsync(cancellationToken);

        return new(jwt.AccessToken, refreshToken, ((DateTimeOffset)jwt.Expires).ToUnixTimeSeconds());
    }
}
