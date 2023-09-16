﻿using Microsoft.AspNetCore.Hosting;
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

                { "BlobStorageSettings:ConnectionString", "mongodb://root:123456@localhost:27017" },
                { "BlobStorageSettings:DatabaseName", "test_db" }
            })
            .Build();
        builder.UseConfiguration(configuration);
    }
}