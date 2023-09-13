using AutoMapper;

using CleanArch.Entities;
using CleanArch.Infrastructure.Contracts.Authentication;

namespace CleanArch.UseCases.Auth.Register;

internal class RegisterCommandProfile : Profile
{
    public RegisterCommandProfile()
    {
        CreateMap<RegisterCommand, User>()
            .ForMember(d => d.Password, o => o.MapFrom<HashedPasswordResolver>());
    }

    private class HashedPasswordResolver : IValueResolver<RegisterCommand, User, string>
    {
        private readonly IPasswordHasher _passwordHasher;

        public HashedPasswordResolver(IPasswordHasher passwordHasher)
        {
            _passwordHasher = passwordHasher;
        }

        public string Resolve(RegisterCommand source, User destination, string destMember, ResolutionContext context)
        {
            return _passwordHasher.Hash(source.Password);
        }
    }
}
