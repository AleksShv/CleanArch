using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using CleanArch.Entities;

namespace CleanArch.DataAccess.SqlServer.Configurations;

internal class BasketTypeConfiguration : IEntityTypeConfiguration<Basket>
{
    public void Configure(EntityTypeBuilder<Basket> builder)
    {
        builder.HasOne(x => x.Customer)
            .WithOne(x => x.Basket)
            .HasForeignKey<Basket>(x => x.CustomerId)
            .OnDelete(DeleteBehavior.Cascade)
            .IsRequired();

        builder.HasMany(x => x.Items)
            .WithOne(x => x.Basket)
            .HasForeignKey(x => x.BasketId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
