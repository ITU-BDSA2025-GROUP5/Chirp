using System.Text;
using Chirp.Tests.Infrastructure;
using Chirp.Infrastructure;
using Chirp.Domain;
using Chirp.Tests.Tools_to_Test;
using Microsoft.AspNetCore.Identity;
using Xunit;
[Collection("sqlite-db")]

public class Cheep_Integration_Test
{
    private readonly CheepDbContext _context;
    private readonly CheepRepository _cheepRepository;
    private readonly UserRepository _userRepository;
    private readonly CheepService _CheepService;
    private readonly UserService _UserService;
private readonly UserManager <User> _UserManager;

    public Cheep_Integration_Test(SqliteInMemoryDbFixture fixture)
    {
        _context = fixture.CreateContext();
        _cheepRepository = new CheepRepository(_context);
        _userRepository = new UserRepository(_context);
        _UserService = new UserService(_userRepository,_UserManager);
        _CheepService = new CheepService(_cheepRepository, _UserService);
    }
    
    [Fact]
    public async Task Get_Cheeps_From_Author_Is_Usable()
    {
        
        var testUser = HelperClasses.createRandomUser();
        _context.Users.Add(testUser);
        await _context.SaveChangesAsync();

        var cheep = HelperClasses.createRandomCheep(testUser);
        _context.Cheeps.Add(cheep);
        await _context.SaveChangesAsync(); 
        
        var Cheeps = await _CheepService.getCheepsFromUser(testUser, 0);
        Assert.NotNull(Cheeps);
        Assert.NotEmpty(Cheeps);
        Assert.Equal(cheep.Text, Cheeps[0].Text);
    }
}