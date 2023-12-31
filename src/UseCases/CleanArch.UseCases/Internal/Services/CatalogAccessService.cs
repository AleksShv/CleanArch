﻿using System.Data;

using Microsoft.EntityFrameworkCore;

using CleanArch.DataAccess.Contracts;
using CleanArch.Entities;
using CleanArch.DomainServices.Catalog.Services;
using CleanArch.UseCases.Internal.Services.Contracts;
using CleanArch.UseCases.Internal.Exceptions;

namespace CleanArch.UseCases.Internal.Services;

internal class CatalogAccessService : ICatalogAccessService
{
    private readonly IApplicationDbContext _context;

    public CatalogAccessService(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task CheckUserProductImageAccessAndThrowAsync(Guid productImageId, Guid userId, UserRole userRole, CancellationToken cancellationToken = default)
    {
        if (!await CheckUserProductImageAccessAsync(productImageId, userId, userRole, cancellationToken))
        {
            throw new ProductImageOwningException(userId, productImageId);
        }
    }

    public async Task CheckUserProductAccessAndThrowAsync(Guid productId, Guid userId, UserRole userRole, CancellationToken cancellationToken = default)
    {
        if (!await CheckUserProductAccessAsync(productId, userId, userRole, cancellationToken))
        {
            throw new ProductOwningException(userId, productId);
        }
    }

    public async Task<bool> CheckUserProductAccessAsync(Guid productId, Guid userId, UserRole userRole, CancellationToken cancellationToken = default)
    {
        var product = await _context.Products
             .AsNoTracking()
             .Where(p => p.Id == productId)
             .Select(p => new Product
             {
                 Id = p.Id,
                 OwnerId = p.OwnerId
             })
             .FirstOrDefaultAsync(cancellationToken)
             ?? throw new ProductNotFoundException(productId);

        return product.CheckProductOwner(userId, userRole);
    }

    public async Task<bool> CheckUserProductImageAccessAsync(Guid productImageId, Guid userId, UserRole userRole, CancellationToken cancellationToken = default)
    {
        var product = await _context.ProductImages
           .AsNoTracking()
           .Where(pi => pi.Id == productImageId)
           .Select(pi => new Product
           {
               Id = pi.ProductId,
               OwnerId = pi.Product.OwnerId
           })
           .FirstOrDefaultAsync(cancellationToken)
           ?? throw new ProductImageNotFoundException(productImageId);

        return product.CheckProductOwner(userId, userRole);
    }
}
