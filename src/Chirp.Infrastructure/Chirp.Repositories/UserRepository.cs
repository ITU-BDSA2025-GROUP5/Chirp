using Chirp.Domain;
using Microsoft.EntityFrameworkCore;

namespace Chirp.Infrastructure;

public class UserRepository : IUserRepository
{
    private readonly CheepDbContext _dbContext;

    public UserRepository(CheepDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<User?> findUserByName(string name)
    {
        return await _dbContext.Users.FirstOrDefaultAsync(u => u.Name == name);
    }

    public async Task<User?> findUserByEmail(string email)
    {
        return await _dbContext.Users.FirstOrDefaultAsync(u => u.Email == email);
    }

    // This method returns a list of all users who's following you. 
    public async Task<List<User>> getFollowers(User user)
    {
        List<User> followers;
        followers = await _dbContext.Follows
            .Where(f => f.FolloweeId == user.UserId)
            .Select(f => f.Follower)
            .ToListAsync();

        return followers;
    }

    // This method return a list of all users who you're following
    public async Task<List<User>> getFollowings(User user)
    {
        var followers = await _dbContext.Follows
            .Where(f => f.FollowerId == user.UserId)
            .Select(f => f.Followee)
            .ToListAsync();

        return followers;
    }
}