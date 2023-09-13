using AutoMapper;
using MediatR;

using CleanArch.DataAccess.Contracts;
using CleanArch.Entities;

namespace CleanArch.UseCases.Catalog.Products.UpdateProductImageOrder;

internal class UpdateProductImageOrderCommandHandler : IRequestHandler<UpdateProductImageOrderCommand, Guid>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public UpdateProductImageOrderCommandHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<Guid> Handle(UpdateProductImageOrderCommand request, CancellationToken cancellationToken)
    {
        var productImage = _mapper.Map<ProductImage>(request);

        _context.ProductImages.Attach(productImage);
        _context.ProductImages.Entry(productImage).Property(x => x.Order).IsModified = true;

        await _context.SaveChangesAsync(cancellationToken);

        return productImage.Id;
    }
}
