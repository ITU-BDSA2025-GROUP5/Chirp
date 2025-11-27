using Chirp.Razor.Tests.Infrastructure;
using Chirp.Infrastructure;
using Chirp.Domain;
using Xunit;
[Collection("sqlite-db")]

public class CheepServiceTests
{
    private readonly CheepDbContext _context;
    private readonly CheepRepo _repo;
    private readonly CheepService _service;
    private readonly UserRepository _userRepository;

    public CheepServiceTests(SqliteInMemoryDbFixture fixture)
    {
        
        _context = fixture.CreateContext();
        _repo = new CheepRepo(_context);
        _service = new CheepService(_repo,_userRepository);
    }
    [Fact]
    public async Task Get_Cheeps_From_Author_Is_Usable()
    {
        //a
        var testUser = new User
        {
            Name = "TestName",
            Email = "TestMail@1234.dk",
            Cheeps = new List<Cheep>(),
            
            /*
            UserId = 0,
           
            ApplicationUserId = "123",
            ApplicationUser = "ssss"
            */
            
        };
        _context.Users.Add(testUser);
        await _context.SaveChangesAsync(); 

        var newCheep = new Cheep
        {
            UserId = "lol123",
            Text = "Test Cheep",
            User = testUser,
            TimeStamp = DateTime.UtcNow
        };

        _context.Cheeps.Add(newCheep);
        await _context.SaveChangesAsync(); 
        var recvCheeps = await _service.getCheepsFromUser(testUser, 0);

        Xunit.Assert.NotNull(recvCheeps);
        Xunit.Assert.NotEmpty(recvCheeps);
        Xunit.Assert.Equal("Test Cheep", recvCheeps[0].Text);
    }
}