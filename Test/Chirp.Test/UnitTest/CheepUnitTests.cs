using Chirp.Domain;
using Chirp.Infrastructure;
using Chirp.Tests.Infrastructure;
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
        
        var testUser = HelperClasses.createRandomUser();
        var cheep = HelperClasses.createRandomCheep(testUser);

        // Instead of _context.Add, insert directly into stub
        await _cheepRepo.InsertNewCheepAsync(cheep);

        var cheeps = await _service.getCheepsFromUser(testUser, 0);

        Assert.NotNull(cheeps);
        Assert.NotEmpty(cheeps);
        Assert.Equal(cheep.Text, cheeps[0].Text);
    }

    [Fact]
    public async Task GetCheepsFromUser_returns_cheeps_from_stub()
    {
        var user = HelperClasses.createRandomUser();
        var cheep = HelperClasses.createRandomCheep(user);
        await _cheepRepo.InsertNewCheepAsync(cheep);

        var cheeps = await _service.getCheepsFromUser(user, 0);

        Assert.Single(cheeps);
        Assert.Equal(cheep.Text, cheeps[0].Text);
    }
    
    [Fact]
    public async Task GetCheepsFromUserIdIsUsable()
    {
        var user = HelperClasses.createRandomUser();
        var cheep = HelperClasses.createRandomCheep(user);
        await _cheepRepo.InsertNewCheepAsync(cheep);
        var cheeps = await _service.GetCheepsFromUserId(user.Id);
        
        Assert.NotNull(cheeps);
        Assert.NotEmpty(cheeps);
        Assert.Equal(cheep.Text, cheeps[0].Text);
    }
}