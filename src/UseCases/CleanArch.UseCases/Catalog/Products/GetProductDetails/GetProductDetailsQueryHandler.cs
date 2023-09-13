using AutoMapper;
using MediatR;

using CleanArch.DataAccess.Contracts;

namespace CleanArch.UseCases.Catalog.Products.GetProductDetails;

internal class GetProductDetailsQueryHandler : IRequestHandler<GetProductDetailsQuery, ProductDetailsDto>
{
    private readonly IProductQueryService _queryService;
    private readonly IMapper _mapper;

    public GetProductDetailsQueryHandler(IProductQueryService queryService, IMapper mapper)
    {
        _queryService = queryService;
        _mapper = mapper;
    }

    public async Task<ProductDetailsDto> Handle(GetProductDetailsQuery request, CancellationToken cancellationToken)
    {
        var product = await _queryService.GetProductDetailsAsync(request.Id);
        var result = _mapper.Map<ProductDetailsDto>(product);

        return result;
    }
}
