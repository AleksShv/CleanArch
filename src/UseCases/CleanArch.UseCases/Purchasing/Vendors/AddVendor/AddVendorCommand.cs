using MediatR;

using CleanArch.UseCases.Common.Requests;

namespace CleanArch.UseCases.Purchasing.Vendors.AddVendor;

public record AddVendorCommand(
    string Name,
    string OGRN,
    string INN,
    string KPP) : IRequest<Guid>, IValidatableRequest;