namespace CleanArch.DomainServices.Catalog.Services;

internal static class SKUGenerator
{
    public static string Generate()
    {
        var rnd = new Random();
        return rnd.NextInt64(9999, 99999).ToString();
    }
}
