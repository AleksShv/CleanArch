namespace CleanArch.Entities;

[Flags]
public enum UserRole
{
    Customer = 0,
    ProductOwner = 1,
    WarehouseWorker = 2,
    Admin = 4
}
