using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using CleanArch.Entities;

namespace CleanArch.DataAccess.SqlServer.Configurations;

internal class WarehouseTypeConfiguration : IEntityTypeConfiguration<Warehouse>
{
    public void Configure(EntityTypeBuilder<Warehouse> builder)
    {
        builder.HasMany(x => x.Supplies)
            .WithOne(x => x.Warehouse)
            .HasForeignKey(x => x.WarehouseId)
            .OnDelete(DeleteBehavior.NoAction);
    }
}
