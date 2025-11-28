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
    private readonly UserRepository _userRepository;
    private readonly CheepService _service;


    public CheepServiceTests(SqliteInMemoryDbFixture fixture)
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
    
    [Fact]
    public async Task GetCheepsFromUser_returns_cheeps_from_db()
    {
        var user = new User { Name = "TestUser", Email = "test@itu.dk",  Cheeps = new List<Cheep>() };
        _context.Users.Add(user);
        await _context.SaveChangesAsync();

        var cheep = new Cheep { UserId = user.Id, Text = "Hello world", User = user };
        _context.Cheeps.Add(cheep);
        await _context.SaveChangesAsync();

        var cheeps = await _service.getCheepsFromUser(user, 0);

        Assert.Single(cheeps);
        Assert.Equal("Hello world", cheeps[0].Text);
    }


    public class CheepServiceUnitTests
    {
        [Fact]
        public async Task GetCheepsFromUser_returns_cheeps_from_repo()
        {
            // Arrange
            var user = new User { Id = Guid.NewGuid(), Name = "TestUser" };
            var cheeps = new List<Cheep>
            {
                new Cheep { UserId = user.Id, Text = "Hello world", User = user }
            };

            var cheepRepoMock = new Mock<ICheepRepo>();
            cheepRepoMock
                .Setup(r => r.GetCheepsFromUserAsync(user, 0))
                .ReturnsAsync(cheeps);

            var userRepoMock = new Mock<IUserRepository>();
            userRepoMock
                .Setup(r => r.FindByIdAsync(user.Id))
                .ReturnsAsync(user);

            var service = new CheepService(cheepRepoMock.Object, userRepoMock.Object);

            // Act
            var result = await service.getCheepsFromUser(user, 0);

            // Assert
            Assert.Single(result);
            Assert.Equal("Hello world", result[0].Text);
        }
    }
    
}