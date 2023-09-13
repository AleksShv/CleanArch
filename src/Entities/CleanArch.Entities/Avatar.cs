using CleanArch.Entities.Base;
using System.Diagnostics.CodeAnalysis;

namespace CleanArch.Entities;

public class Avatar : FileEntity<Guid>
{
    public Guid UserId { get; set; }

    [NotNull]
    public User? User { get; set; }
}
