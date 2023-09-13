using System.Text.Json.Serialization;

using CleanArch.UseCases.Common.Requests;
using CleanArch.UseCases.Warehouses.Warehouses.AddWarehouse;

namespace CleanArch.UseCases.Warehouses.Warehouses.UpdateWarehouse;

public record UpdateWarehouseCommand(
    [property: JsonIgnore] Guid WarehouseId,
    string Name,
    string Location,
    string Address) : AddWarehouseCommand(Name, Location, Address), IValidatableRequest;
