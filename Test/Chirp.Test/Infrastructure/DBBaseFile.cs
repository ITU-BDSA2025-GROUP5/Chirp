using System.Threading.Tasks;
using Chirp.Infrastructure;
using Xunit;

namespace Chirp.Razor.Tests.Infrastructure
{
    [Collection("sqlite-db")]
    public abstract class DbTestBase
    {
        protected readonly SqliteInMemoryDbFixture Fixture;

        protected DbTestBase(SqliteInMemoryDbFixture fixture)
        {
            Fixture = fixture;
        }
        protected CheepDbContext NewContext() => Fixture.CreateContext();

        protected Task ResetDbAsync()
        {
            Fixture.ResetDatabase();
            return Task.CompletedTask;
        }
        protected virtual Task SeedAsync(CheepDbContext db) => Task.CompletedTask;
    }
}
