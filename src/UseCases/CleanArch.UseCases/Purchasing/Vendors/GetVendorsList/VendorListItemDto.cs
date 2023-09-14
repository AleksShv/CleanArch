namespace CleanArch.UseCases.Purchasing.Vendors.GetVendorsList;

public record VendorListItemDto(
    Guid Id,
    string Name,
    string OGRN,
    string INN,
    string KPP);
