namespace CleanArch.Utils;

public static class EnumHelper
{
    public static TEnum[] Values<TEnum>(TEnum @enum, bool skipZero = false) where TEnum : struct, Enum
        => Enum.GetValues<TEnum>()
            .Where(e => @enum.HasFlag(e))
            .Skip(skipZero ? 1 : 0)
            .ToArray();
    public static string[] StringValues<TEnum>(TEnum @enum, bool skipZero = false) where TEnum : struct, Enum
        => Enum.GetValues<TEnum>()
            .Where(e => @enum.HasFlag(e))
            .Select(e => Enum.GetName(typeof(TEnum), e)!)
            .Skip(skipZero ? 1 : 0)
            .ToArray();

    public static string[] StringValues<TEnum>(params TEnum[] enums) where TEnum : struct, Enum
        => enums
            .Where(e => Enum.IsDefined(typeof(TEnum), e))
            .Select(e => Enum.GetName(typeof(TEnum), e)!)
            .ToArray();

    public static TEnum[] FromStringToArray<TEnum>(string values, char separator) where TEnum : struct, Enum
        => values.Split(separator)
            .Where(r => Enum.IsDefined(typeof(TEnum), r))
            .Select(r => (TEnum)Enum.Parse(typeof(TEnum), r))
            .ToArray();

    public static TEnum FromString<TEnum>(string values, char separator) where TEnum : struct, Enum
    {
        var enumType = typeof(TEnum);

        var numeric = values.Split(separator)
            .Where(r => Enum.IsDefined(enumType, r))
            .Select(r => (int)Enum.Parse(enumType, r))
            .Aggregate(0, (x, y) => x | y);

        return (TEnum) Enum.ToObject(enumType, numeric);
    }
}
