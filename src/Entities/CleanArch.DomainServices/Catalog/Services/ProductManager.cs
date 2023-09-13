using CleanArch.Entities;

namespace CleanArch.DomainServices.Catalog.Services;

public static class ProductManager
{
    public static bool CheckProductOwner(this Product product, Guid userId, UserRole userRole)
    {
        if (product.OwnerId == userId)
        {
            return true;
        }

        return userRole.HasFlag(UserRole.Admin);
    }
}
