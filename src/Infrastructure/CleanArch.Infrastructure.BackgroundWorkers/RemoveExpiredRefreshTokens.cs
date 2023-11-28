using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

using CleanArch.DataAccess.Contracts;

namespace CleanArch.Infrastructure.BackgroundWorkers;

internal class RemoveExpiredRefreshTokens : BackgroundService
{
    private readonly IServiceProvider _provider;

    public RemoveExpiredRefreshTokens(IServiceProvider provider)
    {
        _provider = provider;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            using (var scope = _provider.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<IApplicationDbContext>();

                var now = DateTimeOffset.UtcNow;
                await context.RefreshTokens
                    .Where(rt => rt.ExpireAt >= now || rt.IsReleased)
                    .ExecuteDeleteAsync(stoppingToken);
            }
            await Task.Delay(TimeSpan.FromDays(1), stoppingToken);
        }
    }
}
