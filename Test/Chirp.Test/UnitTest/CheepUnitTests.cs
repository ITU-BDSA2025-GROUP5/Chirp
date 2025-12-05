using Chirp.Domain;
using Chirp.Infrastructure;
using Chirp.Tests.Infrastructure;
using Chirp.Tests.Mock_Stub_Classes;
using Chirp.Tests.Tools_to_Test;

namespace Chirp.Tests.UnitTest;

[Collection("sqlite-db")]
public class CheepServiceTests
{
    //fakes for
    private readonly CheepServiceFake _serviceFake;
    private readonly CheepRepositoryFake _cheepRepoFake;
    private readonly UserRepositoryFake _userRepoFake;

    
    //real for isolated tests
    //private readonly CheepService _service;
   // private readonly CheepRepository _cheepRepo;
   // private readonly UserRepositoryFake _userRepo;
    public CheepServiceTests(SqliteInMemoryDbFixture fixture)
    {
        _cheepRepoFake = new CheepRepositoryFake();
        _userRepoFake = new UserRepositoryFake();
        _serviceFake = new CheepServiceFake();
        
        /* unused rn
        _cheepRepo = new CheepRepository();
        _userRepo = new UserRepository();
        _service = new CheepService(_cheepRepo,_userRepo);
        */
    }

    [Fact]
    public async Task Get_Cheeps_From_Author_Is_Usable()
    {
        
        var testUser = HelperClasses.createRandomUser();
        var cheep = HelperClasses.createRandomCheepDTO(testUser);

        await _cheepRepoFake.InsertNewCheepAsync(cheep);

        var cheeps = await _serviceFake.getCheepsFromUser(testUser, 0);

        Assert.NotNull(cheeps);
        Assert.NotEmpty(cheeps);
        Assert.Equal(cheep.Text, cheeps[0].Text);
    }

    [Fact]
    public async Task GetCheepsFromUser_returns_cheeps_from_stub()
    {
        var user = HelperClasses.createRandomUser();
        var cheep = HelperClasses.createRandomCheepDTO(user);
        await _cheepRepoFake.InsertNewCheepAsync(cheep);

        var cheeps = await _serviceFake.getCheepsFromUser(user, 0);

        Assert.Single(cheeps);
        Assert.Equal(cheep.Text, cheeps[0].Text);
    }
    
    [Fact]
    public async Task GetCheepsFromUserIdIsUsable()
    {
        var user = HelperClasses.createRandomUser();
        var cheep = HelperClasses.createRandomCheepDTO(user);
        await _cheepRepoFake.InsertNewCheepAsync(cheep);
        var cheeps = await _serviceFake.GetCheepsFromUserId(user.Id);
        
        Assert.NotNull(cheeps);
        Assert.NotEmpty(cheeps);
        Assert.Equal(cheep.Text, cheeps[0].Text);
    }
}