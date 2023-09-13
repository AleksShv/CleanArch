using System.Data.Common;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

using CleanArch.Entities;

namespace CleanArch.DataAccess.Contracts;

public interface IApplicationDbContext
{
    DbSet<Product> Products { get; }
    DbSet<ProductImage> ProductImages { get; }

    DbSet<Basket> Baskets { get; }
    DbSet<BasketItem> BasketItems { get; }
    DbSet<Order> Orders { get; }
    DbSet<OrderItem> OrderItems { get; }

    DbSet<Vendor> Vendors { get; }
    DbSet<Supply> Supplies { get; }

    DbSet<Warehouse> Warehouses { get; }

    DbSet<User> Users { get; }
    DbSet<RefreshToken> RefreshTokens { get; }
    DbSet<Avatar> Avatars { get; }

    ChangeTracker ChangeTracker { get; }

    DbConnection GetDbConnection();
    Task<DbTransaction> GetTransactionAsync(CancellationToken cancellationToken = default);

    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
