using MediatR;

namespace CleanArch.UseCases.Purchasing.Vendors.GetVendorDetails;

public record GetVendorDetailsQuery(Guid VendorId) : IRequest<VendorDetailsDto?>;
