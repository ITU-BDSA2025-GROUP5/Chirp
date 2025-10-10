using Chirp.Razor.Tests.Infrastructure;

namespace Chirp.Razor.Tests.Integration
{
    public class DbBootSmokeTests : DbTestBase
    {
        public DbBootSmokeTests(SqliteInMemoryDbFixture fixture) : base(fixture) { }

        [Fact]
        public async Task Migration_applies_and_db_is_usable()
        {
            using var db = NewContext();
            var pending = await db.Database.GetPendingMigrationsAsync();
            Assert.Empty(pending);
            Assert.True(await db.Database.CanConnectAsync());
        }
    }
}