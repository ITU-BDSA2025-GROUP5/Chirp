using System.Text;
using Chirp.Razor.Tests.Infrastructure;
using Chirp.Infrastructure;
using Chirp.Domain;
using Xunit;
[Collection("sqlite-db")]

public class CheepServiceTests
{
    private readonly CheepDbContext _context;
    private readonly UserRepository _userRepository;
    private readonly CheepService _service;

    private static readonly Random _rand = new Random();

    public CheepServiceTests(SqliteInMemoryDbFixture fixture)
    {
        _context = fixture.CreateContext();
        var cheepRepo = new CheepRepo(_context);
        _userRepository = new UserRepository(_context);
        _service = new CheepService(cheepRepo, _userRepository);
    }


    
    
    // Generate a random string of given length
    public static string RandomString(int length)
    {
        const string chars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789!@#$%^&*";
        var sb = new StringBuilder(length);
        for (int i = 0; i < length; i++)
        {
            sb.Append(chars[_rand.Next(chars.Length)]);
        }
        return sb.ToString();
    }
    // String mutater
    public static string RandomMutation(string input)
    {
        var chars = input.ToCharArray();
        int idx = _rand.Next(chars.Length);
        chars[idx] = (char)_rand.Next(32, 126); // printable ASCII
        return new string(chars);
    }

    
    
    [Fact]
    public async Task Get_Cheeps_From_Author_Is_Usable()
    {

        var name = RandomString(100);
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
        Xunit.Assert.NotNull(Cheeps);
        Xunit.Assert.NotEmpty(Cheeps);
        Xunit.Assert.Equal("Test Cheep", Cheeps[0].Text);
    }
}