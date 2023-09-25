using System.Text.Json;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using CleanArch.DataAccess.SqlServer.Models;

namespace CleanArch.DataAccess.SqlServer.Configurations;

internal class EntityHistoryConfiguration : IEntityTypeConfiguration<EntityHistory>
{
    public void Configure(EntityTypeBuilder<EntityHistory> builder)
    {
        builder.HasKey(x => x.Id);

        var options = new JsonSerializerOptions
        {
            WriteIndented = false
        };

        builder.Property(x => x.Payload)
            .HasConversion(
                v => JsonSerializer.Serialize(v, options),
                v => JsonSerializer.Deserialize<Dictionary<string, object>>(v, options));

        //builder.OwnsOne(x => x.Payload, b =>
        //{
        //    b.ToJson(nameof(EntityHistory.Payload));
        //});
    }
}
