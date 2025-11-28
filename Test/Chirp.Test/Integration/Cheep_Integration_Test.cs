using System.Text;
using Chirp.Razor.Tests.Infrastructure;
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
        var cheepRepo = new CheepRepo(_context);
        _userRepository = new UserRepository(_context);
        _service = new CheepService(cheepRepo, _userRepository);
    }
    
    [Fact]
    public async Task Get_Cheeps_From_Author_Is_Usable()
    {

        var name = InputFuzzers.RandomString(100);
        //a
        var testUser = new User
        {
            Name = name,
            Email = "TestMail@1234.dk",
            Cheeps = new List<Cheep>(),
            
        };
        _context.Users.Add(testUser);
        await _context.SaveChangesAsync(); 

        var newCheep = new Cheep
        {
            UserId = testUser.Id,
            Text = "Test Cheep",
            User = testUser,
            TimeStamp = DateTime.UtcNow
        };

        _context.Cheeps.Add(newCheep);
        await _context.SaveChangesAsync(); 
        
        var Cheeps = await _service.getCheepsFromUser(testUser, 0);
        Assert.NotNull(Cheeps);
        Assert.NotEmpty(Cheeps);
        Assert.Equal("Test Cheep", Cheeps[0].Text);
    }
}