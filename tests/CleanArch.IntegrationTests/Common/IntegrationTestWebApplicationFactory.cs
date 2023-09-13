using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Configuration;

using CleanArch.DataAccess.SqlServer;

namespace CleanArch.IntegrationTests.Common;

public class IntegrationTestWebApplicationFactory<TProgram> : WebApplicationFactory<TProgram>
    where TProgram : class
{
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureServices((context, services) =>
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseSqlServer(context.Configuration.GetConnectionString("IntegrationTests"))
                .Options;

            var ctxDescriptor = new ServiceDescriptor(
                serviceType: typeof(DbContextOptions<ApplicationDbContext>),
                factory: (_) => options,
                lifetime: ServiceLifetime.Scoped);

            services.Replace(ctxDescriptor);
        });
    }
}