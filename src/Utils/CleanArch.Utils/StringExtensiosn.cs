using System.Diagnostics.CodeAnalysis;

namespace CleanArch.Utils;

public static class StringExtensiosn
{
    public static string Concat(params string[] strs)
        => string.Concat(strs);

    public static string Join(this string[] strs, char separator)
        => string.Join(separator, strs);

    public static string Join(this string[] strs, string separator)
        => string.Join(separator, strs);

    public static string JoinWithComma(this string[] strs)
        => string.Join(',', strs);

    public static bool IsNullOrWhiteSpaces([NotNullWhen(true)] this string? str)
        => string.IsNullOrWhiteSpace(str);

    public static bool IsNullOrEmpty([NotNullWhen(true)] this string? str)
        => string.IsNullOrEmpty(str);
}
