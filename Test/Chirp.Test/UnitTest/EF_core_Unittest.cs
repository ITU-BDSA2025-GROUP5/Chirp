using Chirp.Infrastructure;
using Chirp.Tests.Infrastructure;

namespace Chirp.Tests.UnitTest;



/*
 *
 *Various parts of your system can be replaced with test doubles to test a targeted unit in isolation:

    services
    repositories
    database contexts
    databases
    ...

 *
 * 
 */
public class EF_core_Unittest
{


    private readonly CheepDbContext _context;
    private readonly UserRepository _userRepository;
    private readonly CheepService _service;


    public void EF_Core_Testing_SetUp(SqliteInMemoryDbFixture fixture)
    {
        var _context = fixture.CreateContext();
        var cheepRepo = new CheepRepository(_context);
        var _userRepository = new UserRepository(_context);
        var _service = new CheepService(cheepRepo, _userRepository);

    }
}
/*
    //unit test
    [Fact]
    public async Task  SomeUnitTestOnChatService()
    {
        //taken from session 7

        // Arrange
        IChatRepository chatRepo = new ChatRepositoryStub(...); // not the repository class used in production!
        IChatService service = new ChatService(chatRepo);

        // Act
        service.CreateMessage(...);

        // Assert
        ...


    }



    [Fact]
    public async Task ConnectiontoDp()
    {
        // Arrange
        using var connection = new SqliteConnection("Filename=:memory:");
        await connection.OpenAsync();
        var builder = new DbContextOptionsBuilder<CheepDbContext>().UseSqlite(connection);

        using var context = new CheepDbContext(builder.Options);
        await context.Database.EnsureCreatedAsync(); // Applies the schema to the database

         var CheepRepo = new CheepRepository(context);

        // Act
        var result = repository.QueryMessages("TestUser");
        ...


    }
}
*/