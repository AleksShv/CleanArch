using MediatR;

namespace CleanArch.UseCases.Purchasing.Vendors.RemoveVendor;

public record RemoveVendorCommand(Guid VendorId) : IRequest;
