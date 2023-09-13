namespace CleanArch.DomainServices.Catalog.Consts;

public static class ProductImageConsts
{
    public const long MaxSize = 10 * 1024 * 1024;
    public static readonly string[] ValidExtensions = { ".png", ".jpg", ".jpeg" }; 
}
