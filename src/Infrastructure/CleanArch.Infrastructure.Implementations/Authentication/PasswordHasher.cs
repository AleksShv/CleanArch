using System.Security.Cryptography;

using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using CleanArch.Infrastructure.Contracts.Authentication;

namespace CleanArch.Infrastructure.Implementations.Authentication;

internal class PasswordHasher : IPasswordHasher
{
    private const KeyDerivationPrf Pbkdf2Prf = KeyDerivationPrf.HMACSHA1;
    private const int Pbkdf2IterCount = 1000;
    private const int Pbkdf2SubkeyLength = 256 / 8;
    private const int SaltSize = 128 / 8;

    public string Hash(string password)
    {
        if (string.IsNullOrWhiteSpace(password))
        {
            throw new ArgumentNullException(nameof(password));
        }

        byte[] salt = new byte[SaltSize];

        using var rng = RandomNumberGenerator.Create();
        rng.GetBytes(salt);
        byte[] subkey = KeyDerivation.Pbkdf2(password, salt, Pbkdf2Prf, Pbkdf2IterCount, Pbkdf2SubkeyLength);

        var outputBytes = new byte[1 + SaltSize + Pbkdf2SubkeyLength];
        outputBytes[0] = 0x00;
        Buffer.BlockCopy(salt, 0, outputBytes, 1, SaltSize);
        Buffer.BlockCopy(subkey, 0, outputBytes, 1 + SaltSize, Pbkdf2SubkeyLength);

        var hash = Convert.ToBase64String(outputBytes);
        return hash;
    }

    public bool Verify(string hashedPassword, string password)
    {
        if (string.IsNullOrWhiteSpace(hashedPassword))
        {
            return false;
        }
        if (string.IsNullOrWhiteSpace(password))
        {
            throw new ArgumentNullException(nameof(password));
        }

        var hashedPasswordBytes = Convert.FromBase64String(hashedPassword);

        if (hashedPasswordBytes.Length != (1 + SaltSize + Pbkdf2SubkeyLength))
        {
            return false;
        }

        var salt = new byte[SaltSize];
        Buffer.BlockCopy(hashedPasswordBytes, 1, salt, 0, SaltSize);

        var storedSubkey = new byte[Pbkdf2SubkeyLength];
        Buffer.BlockCopy(hashedPasswordBytes, 1 + SaltSize, storedSubkey, 0, Pbkdf2SubkeyLength);

        byte[] actualSubkey = KeyDerivation.Pbkdf2(password, salt, Pbkdf2Prf, Pbkdf2IterCount, Pbkdf2SubkeyLength);
        return CryptographicOperations.FixedTimeEquals(actualSubkey, storedSubkey);
    }
}
