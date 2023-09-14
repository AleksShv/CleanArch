using MediatR;

namespace CleanArch.UseCases.Purchasing.Vendors.GetVendorsList;

public record GetVendorsListQuery : IRequest<VendorListItemDto[]>;
