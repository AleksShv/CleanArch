﻿namespace CleanArch.Infrastructure.Contracts.Authentication;

public interface IPasswordHasher
{
    public string Hash(string password);
    public bool Verify(string hashedPassword, string password);
}
