using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;
using System;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;

namespace Chirp.Razor.Tests.Infrastructure
{
    public class SqliteInMemoryDbFixture : IDisposable
    {
        private readonly SqliteConnection _keepAliveConnection;
        public DbContextOptions<ChirpDbContext> Options { get; }

        public SqliteInMemoryDbFixture()
        {
            _keepAliveConnection = new SqliteConnection("Data Source=:memory:;Cache=Shared");
            _keepAliveConnection.Open();

            Options = new DbContextOptionsBuilder<ChirpDbContext>()
                .UseSqlite(_keepAliveConnection)
                .EnableSensitiveDataLogging()
                .Options;

            using var ctx = CreateContext();
            ctx.Database.EnsureDeleted();
            ctx.Database.Migrate();
        }

        public ChirpDbContext CreateContext() => new ChirpDbContext(Options);

        public void ResetDatabase()
        {
            using var ctx = CreateContext();
            ctx.Database.EnsureDeleted();
            ctx.Database.Migrate();
        }

        public void Dispose()
        {
            _keepAliveConnection.Close();
            _keepAliveConnection.Dispose();
        }
    }
}
