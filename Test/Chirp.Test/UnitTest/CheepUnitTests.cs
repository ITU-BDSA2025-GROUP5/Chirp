using System.Text;
using Chirp.Razor.Tests.Infrastructure;
using Chirp.Infrastructure;
using Chirp.Domain;
using Chirp.Tests.Tools_to_Test;
using Xunit;
[Collection("sqlite-db")]

public class CheepServiceTests
{
    private readonly CheepDbContext _context;
    private readonly UserRepository _userRepo;
    private readonly CheepService _service;
    private readonly CheepRepo _cheepRepo;


    public CheepServiceTests(SqliteInMemoryDbFixture fixture)
    {
        _context = fixture.CreateContext();
        _cheepRepo = new CheepRepo(_context);
        _userRepo = new UserRepository(_context);
        _service = new CheepService(_cheepRepo, _userRepo);
    }

    public User createRandomUser()
    {
        var name = InputFuzzers.RandomString(100);
        var user = new User
        {
            Name = name,
            Email = $"{name}@example.com",
            Cheeps = new List<Cheep>(),
            
        };

        return user;
    }
    
    
    [Fact]
    public async Task Get_Cheeps_From_Author_Is_Usable()
    {

        var testUser = createRandomUser();
       
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
        Xunit.Assert.NotNull(Cheeps);
        Xunit.Assert.NotEmpty(Cheeps);
        Xunit.Assert.Equal("Test Cheep", Cheeps[0].Text);
        Xunit.Assert.True(Cheeps.Count < 2);   
    }

    [Fact]
    public async Task Get_Cheeps_From_Email_Is_Usable()
    {
    }
}