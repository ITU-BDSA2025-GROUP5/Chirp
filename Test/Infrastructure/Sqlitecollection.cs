using Xunit;

namespace Chirp.Razor.Tests.Infrastructure
{
    [CollectionDefinition("sqlite-db")]
    public class SqliteCollection : ICollectionFixture<SqliteInMemoryDbFixture>
    {
    }
}