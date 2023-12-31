﻿using AutoMapper;

using CleanArch.Controllers.Catalog.Products.Requests;
using CleanArch.Controllers.Catalog.Products.Responses;
using CleanArch.Controllers.Common;
using CleanArch.UseCases.Catalog.Products.AddProduct;
using CleanArch.UseCases.Catalog.Products.GetProductDetails;
using CleanArch.UseCases.Catalog.Products.GetProductsPage;
using CleanArch.UseCases.Catalog.Products.UpdateProduct;
using CleanArch.UseCases.Catalog.Products.UpdateProductImageOrder;
using CleanArch.UseCases.Common.Models;
using CleanArch.Utils.AutoMapper;

namespace CleanArch.Controllers.Catalog.Products.Utils;

internal class ProductMappingProfile : Profile
{
    public ProductMappingProfile()
    {
        CreateMap<GetProductPageRequest, GetProductsPageQuery>()
            .ValueTransformers.Add(MappingValueTransformers.Trimmer);

        CreateMap<PaggingDto<ProductPaggingItemDto>, PaggingResponse<ProductPaggingItemResponse>>()
            .ForRecordParam(d => d.TotalPages, o => o.MapFrom(s => s.TotalPages));
        CreateMap<ProductPaggingItemDto, ProductPaggingItemResponse>();
        CreateMap<ImagePaggingItemDto, ProductImagePaggingItemResponse>();

        CreateMap<ProductDetailsDto, ProductDetailsResponse>();
        CreateMap<ProductImageDetailsDto, ProductImageDetailsResponse>();
        CreateMap<ProductOwnerDetailsDto, ProductOwnerDetailsResponse>();

        CreateMap<AddProductRequest, AddProductCommand>()
            .ValueTransformers.Add(MappingValueTransformers.Trimmer);

        CreateMap<UpdateProductRequest, UpdateProductCommand>()
            .ValueTransformers.Add(MappingValueTransformers.Trimmer);

        CreateMap<UpdateProductImageOrderRequest, UpdateProductImageOrderCommand>();
    }
}
