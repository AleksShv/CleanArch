using MediatR;
using Microsoft.Extensions.DependencyInjection;

using CleanArch.DataAccess.Contracts;

namespace CleanArch.IntegrationTests.Base;

[Collection("Db")]
public abstract class IntegrationTestBase : IClassFixture<IntegrationTestWebApplicationFactory>, IAsyncLifetime, IDisposable
{
    protected readonly IntegrationTestWebApplicationFactory ApplicationFactory;

    protected IntegrationTestBase(IntegrationTestWebApplicationFactory factory)
    {
        ApplicationFactory = factory;
    }

    public async Task ExecuteScopeAsync(Func<IServiceProvider, Task> action)
    {
        using var scope = ApplicationFactory.Services.CreateScope();
        await action(scope.ServiceProvider);
    }
    public async Task<T> ExecuteScopeAsync<T>(Func<IServiceProvider, Task<T>> action)
    {
        using var scope = ApplicationFactory.Services.CreateScope();
        return await action(scope.ServiceProvider);
    }

    public Task ExecuteDbContextAsync(Func<IApplicationDbContext, Task> action)
        => ExecuteScopeAsync(sp => action(sp.GetRequiredService<IApplicationDbContext>()));
    public Task<T> ExecuteDbContextAsync<T>(Func<IApplicationDbContext, Task<T>> action)
        => ExecuteScopeAsync(sp => action(sp.GetRequiredService<IApplicationDbContext>()));

    public Task SendAsync<TRequest>(TRequest request) where TRequest : IRequest
        => ExecuteScopeAsync(sp => sp.GetRequiredService<ISender>().Send(request));
    public Task<TResponse> SendAsync<TRequest, TResponse>(TRequest request) where TRequest : IRequest<TResponse>
        => ExecuteScopeAsync(sp => sp.GetRequiredService<ISender>().Send(request));

    public virtual Task InitializeAsync()
        => Task.CompletedTask;

    public async Task DisposeAsync()
        => await ApplicationFactory.DisposeAsync();

    public virtual void Dispose()
        => ApplicationFactory.Dispose();
}
