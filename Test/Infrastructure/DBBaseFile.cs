falseusing System.Threading.Tasks;
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
        protected ChirpDbContext NewContext() => Fixture.CreateContext();

        protected Task ResetDbAsync()
        {
            Fixture.ResetDatabase();
            return Task.CompletedTask;
        }
        protected virtual Task SeedAsync(ChirpDbContext db) => Task.CompletedTask;
    }
}
