using System.Data.Common;
using System.Reflection;
using System.Linq.Expressions;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

using CleanArch.DataAccess.Contracts;
using CleanArch.Entities;
using CleanArch.DataAccess.SqlServer.Models;
using CleanArch.Entities.Base;

namespace CleanArch.DataAccess.SqlServer;

internal class ApplicationDbContext : DbContext, IApplicationDbContext
{
    private DbTransaction? _transaction;
    private DbConnection? _connection;

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    { }

    internal int TenantId { get; set; }

    public DbSet<Product> Products { get; init; }
    public DbSet<ProductImage> ProductImages { get; init; }

    public DbSet<Basket> Baskets { get; init; }
    public DbSet<BasketItem> BasketItems { get; init; }
    public DbSet<Order> Orders { get; init; }
    public DbSet<OrderItem> OrderItems { get; init; }

    public DbSet<Vendor> Vendors { get; init; }
    public DbSet<Supply> Supplies { get; init; }

    public DbSet<Warehouse> Warehouses { get; init; }
    public DbSet<ProductWarehouse> ProductWarehouses { get; init; }

    public DbSet<User> Users { get; init; }
    public DbSet<Avatar> Avatars { get; init; }
    public DbSet<RefreshToken> RefreshTokens { get; init; }

    public DbSet<EntityHistory> EntityHistories { get; init; }

    public async Task<DbTransaction> GetTransactionAsync(CancellationToken cancellationToken = default)
    {
        if (_transaction is not null)
        {
            return _transaction;
        }

        var ctxTransaction = await Database.BeginTransactionAsync(cancellationToken);
        _transaction = ctxTransaction.GetDbTransaction();

        return _transaction;
    }

    public DbConnection GetDbConnection()
    {
        if (_connection is not null)
        {
            return _connection;
        }

        _connection = Database.GetDbConnection();
        return _connection;
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        var tenatExp = Expression.Constant(TenantId);

        foreach (var entityType in modelBuilder.Model.GetEntityTypes())
        {
            if (typeof(ITenantEntity).IsAssignableFrom(entityType.ClrType))
            {
                var param = Expression.Parameter(entityType.ClrType, "x");
                var property = Expression.Property(param, nameof(ITenantEntity.TenantId));
                var lambdaExp = Expression.Lambda(Expression.Equal(property, tenatExp), param);

                entityType.SetQueryFilter(lambdaExp);
            }
            else
            {
                throw new InvalidOperationException($"Entity {entityType.ClrType.FullName} not assignable to {typeof(ITenantEntity).FullName} interface");
            }
        }
    }
}