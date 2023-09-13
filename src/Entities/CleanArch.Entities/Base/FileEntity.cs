namespace CleanArch.Entities.Base;

public abstract class FileEntity<TKey> : Entity<TKey>
    where TKey : IEquatable<TKey>
{
    public string FileName { get; set; } = default!;
    public string Ext { get; set; } = default!;
    public long Size { get; set; }
}
