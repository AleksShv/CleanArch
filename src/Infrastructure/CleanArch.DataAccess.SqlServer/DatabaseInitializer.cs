using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

using CleanArch.Entities;
using CleanArch.Infrastructure.Contracts.Authentication;

namespace CleanArch.DataAccess.SqlServer;

public static class DatabaseInitializer
{
    public static void InitFromMigrations(IServiceProvider provider, bool fromScratch = false)
    {
        using var scope = provider.CreateScope();

        var ctx = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

        if (fromScratch)
        {
            ctx.Database.EnsureDeleted();
        }

        ctx.Database.Migrate();
    }

    public static void SeedData(IServiceProvider provider)
    {
        using var scope = provider.CreateScope();

        var ctx = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        
        var password = scope.ServiceProvider.GetRequiredService<IPasswordHasher>().Hash("123456");

        if (!ctx.Users.Any())
        {
            var user = new User[]
            {
                new User
                {
                    Email = "admin@mail.test.com",
                    FirstName = "admin",
                    Role = UserRole.Admin,
                    Password = password
                },
                new User
                {
                    Email = "productowner@mail.test.com",
                    FirstName = "productowner",
                    Role = UserRole.ProductOwner,
                    Password = password
                },
                new User
                {
                    Email = "warehouse_worker@mail.test.com",
                    FirstName = "warehouse worker",
                    Role = UserRole.WarehouseWorker,
                    Password = password
                },
                new User
                {
                    Email = "customer@mail.test.com",
                    FirstName = "customer",
                    Role = UserRole.Customer,
                    Password = password
                },
            };
            ctx.AddRange(user);
            ctx.SaveChanges();
        }
        
        if (!ctx.Warehouses.Any())
        {
            var warehouses = new Warehouse[]
            {
                new Warehouse
                {
                  Name = "Southern Warehouse",
                  Address = "some southern address",
                  Location = "South"
                },
                new Warehouse
                {
                  Name = "Nortthern Warehouse",
                  Address = "some northen address",
                  Location = "North"
                },
                new Warehouse
                {
                  Name = "East Warehouse",
                  Address = "some east address",
                  Location = "East"
                },
            };
            ctx.AddRange(warehouses);
            ctx.SaveChanges();
        }

        if (!ctx.Vendors.Any())
        {
            var vendors = new Vendor[]
            {
                new Vendor
                {
                    Name = "Vendor 1",
                    INN = "0245260609",
                    OGRN = "6166138516164",
                    KPP = "668745213",
                },
                new Vendor
                {
                    Name = "Vendor 2",
                    INN = "1683872052",
                    OGRN = "8033325875641",
                    KPP = "761743670"
                },
                new Vendor
                {
                    Name = "Vendor 3",
                    INN = "7099370392",
                    OGRN = "2133711727422",
                    KPP = "653444291"
                }
            };
            ctx.AddRange(vendors);
            ctx.SaveChanges();
        }

        if (!ctx.Products.Any())
        {
            var productId = Guid.NewGuid();
            var productOwnerId = ctx.Users.Where(x => x.Role == UserRole.ProductOwner).Select(x => x.Id).First();
            var vendorId = ctx.Vendors.Select(x => x.Id).First();
            var warehouses = ctx.Warehouses.Select(x => new Warehouse { Id = x.Id }).Take(2).ToList();

            ctx.ChangeTracker.Clear();
            ctx.AttachRange(warehouses);

            var product = new Product
            {
                Id = productId,
                Title = "product #1",
                Description = "description for product #1",
                Price = 15.00m,
                OwnerId = productOwnerId,
                Warehouses = warehouses,
                VendorId = vendorId,
                Cost = 10.00m,
                IsAvailableForSale = true,
                QuantityInStock = 10,
                SKU = "PF10",
                Supplies = new List<Supply>
                {
                    new Supply
                    {
                        IsCompleted = true,
                        Quantity = 3,
                        VendorId = vendorId,
                        WarehouseId = warehouses[0].Id
                    },
                    new Supply
                    {
                        IsCompleted = true,
                        Quantity = 7,
                        VendorId = vendorId,
                        WarehouseId = warehouses[1].Id
                    }
                },
                ProductWarehouses = new List<ProductWarehouse> 
                { 
                    new ProductWarehouse 
                    {
                        Quantity = 3,
                        WarehouseId = warehouses[0].Id,
                        ProductId = productId,
                    },
                    new ProductWarehouse
                    {
                        Quantity = 7,
                        WarehouseId = warehouses[1].Id,
                        ProductId = productId,
                    }
                }
            };

            ctx.Add(product);
            ctx.SaveChanges();
        }
    }
}
