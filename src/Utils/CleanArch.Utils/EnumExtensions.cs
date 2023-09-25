namespace CleanArch.Utils;

public static class EnumExtensions
{
    public static string? GetName<TEnum>(this TEnum @enum)
        where TEnum : struct, Enum
        => Enum.GetName<TEnum>(@enum);
}
