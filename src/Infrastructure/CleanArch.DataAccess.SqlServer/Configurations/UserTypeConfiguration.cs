using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using CleanArch.Entities;

namespace CleanArch.DataAccess.SqlServer.Configurations;

internal class UserTypeConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasOne(x => x.Avatar)
            .WithOne(x => x.User)
            .HasForeignKey<Avatar>(x => x.UserId)
            .IsRequired();

        builder.HasMany(x => x.RefreshTokens)
            .WithOne(x => x.User)
            .HasForeignKey(x => x.UserId)
            .OnDelete(DeleteBehavior.NoAction);
    }
}
