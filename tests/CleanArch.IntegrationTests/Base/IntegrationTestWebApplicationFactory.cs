using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Configuration;

namespace CleanArch.IntegrationTests.Base;

[Collection("Db")]
public class IntegrationTestWebApplicationFactory : WebApplicationFactory<Program>
{
    private readonly DbFixture _fixture;

    public IntegrationTestWebApplicationFactory(DbFixture fixture)
    {
        _fixture = fixture;
    }

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.UseEnvironment("Development");

        var configuration = new ConfigurationBuilder()
            .AddInMemoryCollection(new Dictionary<string, string?>
            {
                { "ConnectionStrings:SqlServer", _fixture.ConnectionString },
                { "ConnectionStrings:Redis", "localhost:6379,password=123456" },

                { "BlobStorageSettings:ConnectionString", "mongodb://root:123456@localhost:27017" },
                { "BlobStorageSettings:DatabaseName", "test_db" },

                { "MultiTenancySettings:DefaultConnection", _fixture.ConnectionString },
                { "MultiTenancySettings:Tenants:1:ConnectionString", null },
                { "MultiTenancySettings:Tenants:2:ConnectionString", null }
            })
            .Build();
        builder.UseConfiguration(configuration);
    }
}