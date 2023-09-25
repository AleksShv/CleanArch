using System.ComponentModel.DataAnnotations;

namespace CleanArch.Entities.Base;

public abstract class Entity<T> : IEquatable<Entity<T>>
    where T : IEquatable<T>
{
    private int? _hashCode;

    [Key]
    public T Id { get; set; } = default!;

    public bool IsNew => Id.Equals(default);

    public override bool Equals(object? obj)
        => Equals(obj as Entity<T>);

    public bool Equals(Entity<T>? other)
    {
        if (other is null) return this is null;
        if (ReferenceEquals(other, this)) return true;
        return Id.Equals(other.Id);
    }

    public override int GetHashCode()
    {
        if (!_hashCode.HasValue)
        {
            _hashCode = HashCode.Combine(Id, GetType());
        }
        return _hashCode.Value;
    }

    public override string ToString()
        => string.Format("{0}: {1}", GetType().Name, Id);

    public static bool operator ==(Entity<T> left, Entity<T> right)
        => left.Equals(right);

    public static bool operator !=(Entity<T> left, Entity<T> right)
        => !left.Equals(right);
}