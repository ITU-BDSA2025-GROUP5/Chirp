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
   // private readonly CheepServiceFake _CheepserviceFake;

    //real for testing
    
    
    //private readonly CheepService _service;
   // private readonly CheepRepository _cheepRepo;
   // private readonly UserRepositoryFake _userRepo;
   private readonly CheepRepository _realCheepRepo;
    private readonly CheepDbContext _context;

    public CheepServiceTests(SqliteInMemoryDbFixture fixture)
    {
        _context = fixture.CreateContext(); 
        _realCheepRepo = new CheepRepository(_context);

        // ... existing fake setup code remains
        var userRepoFake = new UserRepositoryFake();
        var cheepRepoFake = new CheepRepositoryFake();
        var userServiceFake = new UserServiceFake(userRepoFake); 
       // var CheepserviceFake = new CheepServiceFake(cheepRepoFake, userServiceFake);
    }

    [Fact]
    public async Task GetCheepsFromUserId_ReturnsCorrectCheepsForUser()
    {
        // Arrange
        // Create a user and add to database
        var user = HelperClasses.createRandomUser();
        await _context.Users.AddAsync(user);
        await _context.SaveChangesAsync();
        
        // Create cheeps for this user
        var cheep1 = new Cheep 
        { 
            Text = "Test cheep 1",
            UserId = user.Id,
            User = user,
            TimeStamp = DateTime.Now.AddMinutes(-10)
        };
        
        var cheep2 = new Cheep 
        { 
            Text = "Test cheep 2",
            UserId = user.Id,
            User = user,
            TimeStamp = DateTime.Now.AddMinutes(-5)
        };
        
        // Create another user with different cheeps
        var otherUser = HelperClasses.createRandomUser();
        await _context.Users.AddAsync(otherUser);
        await _context.SaveChangesAsync();
        
        var otherCheep = new Cheep 
        { 
            Text = "Other user's cheep",
            UserId = otherUser.Id,
            User = otherUser,
            TimeStamp = DateTime.Now
        };
        
        // Insert all cheeps
        await _context.Cheeps.AddAsync(cheep1);
        await _context.Cheeps.AddAsync(cheep2);
        await _context.Cheeps.AddAsync(otherCheep);
        await _context.SaveChangesAsync();
        
        // Act
        var result = await _realCheepRepo.getCheepsFromUserId(user.Id, 1);
        
        // Assert
        Assert.NotNull(result);
        Assert.Equal(2, result.Count);
        Assert.All(result, cheep => Assert.Equal(user.Id, cheep.User.Id));
        // Should be ordered by timestamp descending (newest first)
        Assert.True(result[0].TimeStamp >= result[1].TimeStamp);
    }

    [Fact]
    public async Task GetCheepsFromUserId_ReturnsEmptyListWhenUserHasNoCheeps()
    {
        // Arrange
        var user = HelperClasses.createRandomUser();
        await _context.Users.AddAsync(user);
        await _context.SaveChangesAsync();
        
        // Create another user with cheeps to ensure we're filtering correctly
        var otherUser = HelperClasses.createRandomUser();
        await _context.Users.AddAsync(otherUser);
        await _context.SaveChangesAsync();
        
        var otherCheep = new Cheep 
        { 
            Text = "Other user's cheep",
            UserId = otherUser.Id,
            User = otherUser,
            TimeStamp = DateTime.Now
        };
        await _context.Cheeps.AddAsync(otherCheep);
        await _context.SaveChangesAsync();
        
        // Act
        var result = await _realCheepRepo.getCheepsFromUserId(user.Id, 1);
        
        // Assert
        Assert.NotNull(result);
        Assert.Empty(result);
    }

    [Fact]
    public async Task GetCheepsFromUserId_ReturnsEmptyListForNonExistentUserId()
    {
        // Arrange
        var nonExistentUserId = "non-existent-user-id";
        
        // Act
        var result = await _realCheepRepo.getCheepsFromUserId(nonExistentUserId, 1);
        
        // Assert
        Assert.NotNull(result);
        Assert.Empty(result);
    }

    [Fact]
    public async Task GetCheepsFromUserId_PageNumberLessThanOne_ReturnsFirstPage()
    {
        // Arrange
        var user = HelperClasses.createRandomUser();
        await _context.Users.AddAsync(user);
        await _context.SaveChangesAsync();
        
        // Create a few cheeps
        var cheep1 = new Cheep 
        { 
            Text = "Test cheep 1",
            UserId = user.Id,
            User = user,
            TimeStamp = DateTime.Now.AddMinutes(-10)
        };
        
        var cheep2 = new Cheep 
        { 
            Text = "Test cheep 2",
            UserId = user.Id,
            User = user,
            TimeStamp = DateTime.Now.AddMinutes(-5)
        };
        
        await _context.Cheeps.AddAsync(cheep1);
        await _context.Cheeps.AddAsync(cheep2);
        await _context.SaveChangesAsync();
        
        // Act - page number 0 should be treated as page 1
        var result = await _realCheepRepo.getCheepsFromUserId(user.Id, 0);
        
        // Assert
        Assert.NotNull(result);
        Assert.Equal(2, result.Count);
    }

    [Fact]
    public async Task GetCheepsFromUserId_IncludesUserInformation()
    {
        // Arrange
        var user = HelperClasses.createRandomUser();
        await _context.Users.AddAsync(user);
        await _context.SaveChangesAsync();
        
        var cheep = new Cheep 
        { 
            Text = "Test cheep",
            UserId = user.Id,
            User = user,
            TimeStamp = DateTime.Now
        };
        await _context.Cheeps.AddAsync(cheep);
        await _context.SaveChangesAsync();
        
        // Act
        var result = await _realCheepRepo.getCheepsFromUserId(user.Id, 1);
        
        // Assert
        Assert.NotNull(result);
        Assert.Single(result);
        Assert.NotNull(result[0].User);
        Assert.Equal(user.Id, result[0].User.Id);
        Assert.Equal(user.UserName, result[0].User.UserName);
        Assert.Equal(user.Email, result[0].User.Email);
    }

    // Cleanup after each test
    [Fact]
    public async Task DisposeAsync()
    {
        await _context.Database.EnsureDeletedAsync();
        await _context.DisposeAsync();
    }
}