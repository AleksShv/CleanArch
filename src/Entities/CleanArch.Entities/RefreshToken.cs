using System.Diagnostics.CodeAnalysis;

using CleanArch.Entities.Base;

namespace CleanArch.Entities;

public class RefreshToken : Entity<Guid>
{
    public Guid UserId { get; set; }

    [NotNull]
    public User? User { get; set; }

    public string Token { get; set; } = default!;
    public bool IsReleased { get; set; } = default!;
    public DateTimeOffset CreatedAt { get; set; }
    public DateTimeOffset ExpireAt { get; set; }
}
