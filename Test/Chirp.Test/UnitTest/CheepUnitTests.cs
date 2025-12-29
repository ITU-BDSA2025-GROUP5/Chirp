using Chirp.Domain;
using Chirp.Infrastructure;
using Chirp.Tests.Infrastructure;
using Chirp.Tests.Mock_Stub_Classes;
using Chirp.Tests.Tools_to_Test;

namespace Chirp.Tests.UnitTest;

[Collection("sqlite-db")]
public class CheepServiceTests
{
    //fakes for isolated testing
    private readonly CheepServiceFake _CheepserviceFake;
    private readonly CheepRepositoryFake _CheepRepoFake;
    private readonly UserRepositoryFake _userRepoFake;
    private readonly UserServiceFake _userServiceFake;

    //real for testing
    
    
    //private readonly CheepService _service;
   // private readonly CheepRepository _cheepRepo;
   // private readonly UserRepositoryFake _userRepo;
    public CheepServiceTests(SqliteInMemoryDbFixture fixture)
    {
     
        _userRepoFake = new UserRepositoryFake();
        _CheepRepoFake = new CheepRepositoryFake();
        _userServiceFake = new UserServiceFake(_userRepoFake); 
       
        _CheepserviceFake = new CheepServiceFake(_CheepRepoFake,_userServiceFake);
        
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

      
        await _CheepserviceFake.InsertCheepAsync(cheep);

        var cheeps = await _CheepserviceFake.getCheepsFromUser(testUser, 0);

        Assert.NotNull(cheeps);
        Assert.NotEmpty(cheeps);
        Assert.Equal(cheep.Text, cheeps[0].Text);
    }

    [Fact]
    public async Task GetCheepsFromUser_returns_cheeps_from_stub()
    {
        var user = HelperClasses.createRandomUser();
        var cheep = HelperClasses.createRandomCheepDTO(user);
        // Use the service to insert
        await _CheepserviceFake.InsertCheepAsync(cheep);

        var cheeps = await _CheepserviceFake.getCheepsFromUser(user, 0);

        Assert.Single(cheeps);
        Assert.Equal(cheep.Text, cheeps[0].Text);
    }
    
    [Fact]
    public async Task GetCheepsFromUserIdIsUsable()
    {
        var user = HelperClasses.createRandomUser();
        var cheep = HelperClasses.createRandomCheepDTO(user);
        // Use the service to insert
        await _CheepserviceFake.InsertCheepAsync(cheep);
        var cheeps = await _CheepserviceFake.GetCheepsFromUserId(user.Id,0);
        
        Assert.NotNull(cheeps);
        Assert.NotEmpty(cheeps);
        Assert.Equal(cheep.Text, cheeps[0].Text);
    }
}