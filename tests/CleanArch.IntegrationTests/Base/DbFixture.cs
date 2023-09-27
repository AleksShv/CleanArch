using Microsoft.EntityFrameworkCore;

using CleanArch.DataAccess.SqlServer;

namespace CleanArch.IntegrationTests.Base;

public class DbFixture : IDisposable
{
    private ApplicationDbContext? _context;

    private bool _disposed;

    public DbFixture()
    {
        DatabaseName = $"test_db_{Guid.NewGuid()}";
        ConnectionString = $"Server=localhost,1433; Database={DatabaseName}; User Id=SA; Password=AdmiN_123456_; MultipleActiveResultSets=True; TrustServerCertificate=True;";

        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseSqlServer(ConnectionString)
            .Options;

        _context = new ApplicationDbContext(options);
    }

    public string DatabaseName { get; }
    public string ConnectionString { get; }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    protected virtual void Dispose(bool disposing)
    {
        if (!_disposed)
        {
            if (disposing)
            {
                _context?.Database.EnsureDeleted();
                _context?.Dispose();
                _context = null;
            }

            _disposed = true;
        }
    }
}

[CollectionDefinition("Db")]
public class DbFixtureCollection : ICollectionFixture<DbFixture> { }