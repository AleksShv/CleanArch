namespace CleanArch.UseCases.Common.Utils;

internal static class CacheKeyGenerator
{
    private const char Separator = '_';

    public static string GenerateKey<T>(object key)
        => GenerateKey(typeof(T).Name, key);

    public static string GenerateKey(string typeName, object key)
    {
        var ctx = new
        {
            TypeName = typeName,
            Key = key.ToString() ?? string.Empty
        };

        var length = ctx.TypeName.Length + ctx.Key.Length + 1;

        return string.Create(length, ctx, (chars, state) =>
        {
            var position = 0;

            state.TypeName
                .AsSpan()
                .CopyTo(chars);
            position += state.TypeName.Length;

            chars[position++] = Separator;

            state.Key
                .AsSpan()
                .CopyTo(chars[position..]);
        });
    }
}
