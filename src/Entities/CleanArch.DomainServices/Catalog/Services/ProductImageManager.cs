using CleanArch.DomainServices.Catalog.Consts;
using CleanArch.Entities;

namespace CleanArch.DomainServices.Catalog.Services;

public static class ProductImageManager
{
    public static bool IsValidImageExtension(this ProductImage productImage)
    {
        return ProductImageConsts.ValidExtensions.Contains(productImage.Ext);
    }

    public static bool IsValidImageExtension(this string fileName)
    {
        var ext = Path.GetExtension(fileName);
        return ProductImageConsts.ValidExtensions.Contains(ext);
    }
}
