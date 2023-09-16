using System.Linq.Expressions;

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
using CleanArch.Utils;
using CleanArch.Utils.AutoMapper;

namespace CleanArch.Controllers.Catalog.Products.Utils;

internal class ProductMappingProfile : Profile
{
    public ProductMappingProfile()
    {
        Expression<Func<string, string>> trimmer = 
            value => value.IsNullOrWhiteSpaces() ? value : value.Trim();

        CreateMap<GetProductPageRequest, GetProductsPageQuery>()
            .ValueTransformers.Add(trimmer);

        CreateMap<PaggingDto<ProductPaggingItemDto>, PaggingResponse<ProductPaggingItemResponse>>()
            .ForRecordParam(d => d.TotalPages, o => o.MapFrom(s => s.TotalPages));
        CreateMap<ProductPaggingItemDto, ProductPaggingItemResponse>();
        CreateMap<ImagePaggingItemDto, ProductImagePaggingItemResponse>();

        CreateMap<ProductDetailsDto, ProductDetailsResponse>();
        CreateMap<ProductImageDetailsDto, ProductImageDetailsResponse>();
        CreateMap<ProductOwnerDetailsDto, ProductOwnerDetailsResponse>();

        CreateMap<AddProductRequest, AddProductCommand>()
            .ValueTransformers.Add(trimmer);

        CreateMap<UpdateProductRequest, UpdateProductCommand>()
            .ValueTransformers.Add(trimmer);

        CreateMap<UpdateProductImageOrderRequest, UpdateProductImageOrderCommand>();
    }
}
