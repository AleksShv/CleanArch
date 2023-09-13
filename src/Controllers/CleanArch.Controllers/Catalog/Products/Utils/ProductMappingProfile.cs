using AutoMapper;
using CleanArch.Controllers.Catalog.Products.Requests;
using CleanArch.Controllers.Catalog.Products.Responses;
using CleanArch.Controllers.Common;
using CleanArch.UseCases.Catalog.Products.AddProduct;
using CleanArch.UseCases.Catalog.Products.GetProductDetails;
using CleanArch.UseCases.Catalog.Products.GetProductsPage;
using CleanArch.UseCases.Catalog.Products.UpdateProduct;
using CleanArch.UseCases.Catalog.Products.UpdateProductImageOrder;
using CleanArch.UseCases.Common.Models;

namespace CleanArch.Controllers.Catalog.Products.Utils;

internal class ProductMappingProfile : Profile
{
    public ProductMappingProfile()
    {
        CreateMap<GetProductPageRequest, GetProductsPageQuery>();

        CreateMap<PaggingDto<ProductPaggingItemDto>, PaggingResponse<ProductPaggingItemResponse>>();
        CreateMap<ProductPaggingItemDto, ProductPaggingItemResponse>();
        CreateMap<ImagePaggingItemDto, ProductImagePaggingItemResponse>();

        CreateMap<ProductDetailsDto, ProductDetailsResponse>();
        CreateMap<ProductImageDetailsDto, ProductImageDetailsResponse>();
        CreateMap<ProductOwnerDetailsDto, ProductOwnerDetailsResponse>();

        CreateMap<AddProductRequest, AddProductCommand>();

        CreateMap<UpdateProductRequest, UpdateProductCommand>();

        CreateMap<UpdateProductImageOrderRequest, UpdateProductImageOrderCommand>();
    }
}
