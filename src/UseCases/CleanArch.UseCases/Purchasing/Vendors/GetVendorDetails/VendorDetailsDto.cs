namespace CleanArch.UseCases.Purchasing.Vendors.GetVendorDetails;

public record VendorDetailsDto(
    Guid Id,
    string Name,
    string OGRN,
    string INN,
    string KPP);
