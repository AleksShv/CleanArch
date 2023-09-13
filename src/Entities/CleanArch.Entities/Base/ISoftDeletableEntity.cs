using System.Diagnostics.CodeAnalysis;

namespace CleanArch.Entities.Base;

public interface ISoftDeletableEntity
{
    [MemberNotNullWhen(true, nameof(DeletedBy))]
    [MemberNotNullWhen(true, nameof(DeletedAt))]
    public bool IsDeleted { get; set; }

    public string? DeletedBy { get; set; }
    public DateTimeOffset? DeletedAt { get; set; }
}
