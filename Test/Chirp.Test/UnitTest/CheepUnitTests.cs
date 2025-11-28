using Chirp.Domain;
using Chirp.Infrastructure;
using Chirp.Razor.Tests.Infrastructure;
using Chirp.Tests.Mock_Stub_Classes;
using Chirp.Tests.Tools_to_Test;

namespace Chirp.Tests.UnitTest;

[Collection("sqlite-db")]
public class CheepServiceTests
{
    private readonly CheepService _service;
    private readonly CheepRepositoryStub _cheepRepo;
    private readonly UserRepositoryStub _userRepo;

    public CheepServiceTests(SqliteInMemoryDbFixture fixture)
    {
        // Use stubs instead of EF Core repos
        _cheepRepo = new CheepRepositoryStub();
        _userRepo = new UserRepositoryStub();
        _service = new CheepService(_cheepRepo, _userRepo);
    }

    [Fact]
    public async Task Get_Cheeps_From_Author_Is_Usable()
    {
        var name = InputFuzzers.RandomString(100);
        var testUser = new User
        {
            Id = Guid.NewGuid().ToString(),
            Name = name,
            Email = "TestMail@1234.dk",
            Cheeps = new List<Cheep>()
        };

        // Instead of _context.Add, insert directly into stub
        await _cheepRepo.InsertNewCheepAsync(new CheepDTO
        {
            Text = "Test Cheep",
            User = testUser,
            TimeStamp = DateTime.UtcNow
        });

        var cheeps = await _service.getCheepsFromUser(testUser, 0);

        Assert.NotNull(cheeps);
        Assert.NotEmpty(cheeps);
        Assert.Equal("Test Cheep", cheeps[0].Text);
    }

    [Fact]
    public async Task GetCheepsFromUser_returns_cheeps_from_stub()
    {
        var user = new User
        {
            Id = Guid.NewGuid().ToString(),
            Name = "TestUser",
            Email = "test@itu.dk",
            Cheeps = new List<Cheep>()
        };

        await _cheepRepo.InsertNewCheepAsync(new CheepDTO
        {
            Text = "Hello world",
            User = user,
            TimeStamp = DateTime.UtcNow
        });

        var cheeps = await _service.getCheepsFromUser(user, 0);

        Assert.Single(cheeps);
        Assert.Equal("Hello world", cheeps[0].Text);
    }
}