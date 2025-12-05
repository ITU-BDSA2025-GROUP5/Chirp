using System.Text;
using Chirp.Tests.Infrastructure;
using Chirp.Infrastructure;
using Chirp.Domain;
using Chirp.Tests.Tools_to_Test;
using Xunit;
[Collection("sqlite-db")]

public class Cheep_Integration_Test
{
    private readonly CheepDbContext _context;
    private readonly UserRepository _userRepository;
    private readonly CheepService _service;


    public Cheep_Integration_Test(SqliteInMemoryDbFixture fixture)
    {
        _context = fixture.CreateContext();
        var cheepRepo = new CheepRepository(_context);
        _userRepository = new UserRepository(_context);
        _service = new CheepService(cheepRepo, _userRepository);
    }
    
    [Fact]
    public async Task Get_Cheeps_From_Author_Is_Usable()
    {

        var name = InputFuzzers.RandomString(100);
        //a
        var testUser = HelperClasses.createRandomUser();
        _context.Users.Add(testUser);
        await _context.SaveChangesAsync();

        var cheep = HelperClasses.createRandomCheep(testUser);
        _context.Cheeps.Add(cheep);
        await _context.SaveChangesAsync(); 
        
        var Cheeps = await _service.getCheepsFromUser(testUser, 0);
        Assert.NotNull(Cheeps);
        Assert.NotEmpty(Cheeps);
        Assert.Equal("Test Cheep", Cheeps[0].Text);
    }
}