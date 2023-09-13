using Dapper;

using CleanArch.DataAccess.Contracts;
using CleanArch.Entities;

namespace CleanArch.DataAccess.SqlServer.QueryServices;

internal class SqlServerProductQueryService : IProductQueryService
{
    private readonly IApplicationDbContext _context;

    public SqlServerProductQueryService(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Product?> GetProductDetailsAsync(Guid id)
    {
        await using var connection = _context.GetDbConnection();
        
        await connection.OpenAsync();

        var products = await connection.QueryAsync<Product, ProductImage, User, Product>(
            sql: """
                select P.Id, p.Title, P.Description, P.Price, P.SKU, PI.Id, PI.FileName, PI.[Order], U.Id, U.FirstName, U.LastName, U.Email
                from dbo.Products as P
                left join dbo.ProductImages as PI on P.Id = PI.ProductId
                left join dbo.Users as U on u.Id = P.OwnerId
                where p.Id = @id and p.IsDraft = 'False' and p.IsDeleted = 'False' and p.IsAvailableForSale = 'True'
                order by p.Id, PI.[Order]
            """,
            param: new { id },
            map: (p, pi, u) =>
            {
                if (p is not null)
                {
                    p.Images.Add(pi);
                    p.Owner = u;
                }
                return p!;
            },
            splitOn: "Id");

        var product = products
            .GroupBy(x => x.Id)
            .Select(g =>
            {
                var product = g.First();
                product.Images = g.Select(p => p.Images.Single()).ToList();
                return product;
            })
            .FirstOrDefault();

        return product;
    }
}