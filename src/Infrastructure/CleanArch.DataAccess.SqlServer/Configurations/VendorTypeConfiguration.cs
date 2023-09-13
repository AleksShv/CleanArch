using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using CleanArch.Entities;

namespace CleanArch.DataAccess.SqlServer.Configurations;

internal class VendorTypeConfiguration : IEntityTypeConfiguration<Vendor>
{
    public void Configure(EntityTypeBuilder<Vendor> builder)
    {
        builder.HasMany(x => x.Supplies)
            .WithOne(x => x.Vendor)
            .HasForeignKey(x => x.VendorId)
            .OnDelete(DeleteBehavior.NoAction);
    }
}
