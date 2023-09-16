using Dapper;

using CleanArch.DataAccess.Contracts;
using CleanArch.Entities;
using CleanArch.Infrastructure.Contracts.UserProvider;

namespace CleanArch.DataAccess.SqlServer.QueryServices;

internal class SqlServerProductQueryService : IProductQueryService
{
    private readonly IApplicationDbContext _context;
    private readonly ICurrentUserProvider _userProvider;

    public SqlServerProductQueryService(IApplicationDbContext context, ICurrentUserProvider userProvider)
    {
        _context = context;
        _userProvider = userProvider;
    }

    public async Task<Product?> GetProductDetailsAsync(Guid id)
    {
        await using var connection = _context.GetDbConnection();
        
        await connection.OpenAsync();

        var userId = _userProvider.GetUserId<Guid>();

        var products = await connection.QueryAsync<Product, ProductImage, User, Product>(
            sql: """
                select P.Id, p.Title, P.Description, P.Price, P.SKU, PI.Id, PI.FileName, PI.[Order], U.Id, U.FirstName, U.LastName, U.Email
                from dbo.Products as P
                left join dbo.ProductImages as PI on P.Id = PI.ProductId
                left join dbo.Users as U on u.Id = P.OwnerId
                where (p.Id = @id and p.IsDraft = 'False' and p.IsDeleted = 'False' and p.IsAvailableForSale = 'True')
                or (p.Id = @id and p.OwnerId = @userId)
                order by p.Id, PI.[Order]
            """,
            param: new { id, userId },
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