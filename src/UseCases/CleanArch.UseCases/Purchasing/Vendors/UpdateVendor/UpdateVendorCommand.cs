using System.Text.Json.Serialization;

using CleanArch.UseCases.Purchasing.Vendors.AddVendor;

namespace CleanArch.UseCases.Purchasing.Vendors.UpdateVendor;

public record UpdateVendorCommand(
    [property: JsonIgnore] Guid Id,
    string Name,
    string OGRN,
    string INN,
    string KPP) : AddVendorCommand(Name, OGRN, INN, KPP);