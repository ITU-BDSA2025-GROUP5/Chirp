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
    public async Task<List<int>> getFollowings(User user)
    {
        var followers = await _dbContext.Follows
            .Where(f => f.FollowerId == user.UserId)
            .Select(f => f.FolloweeId)
            .ToListAsync();

        return followers;
    }

    public async Task<String> followUser(User user, int followeeID)
    {
        var follow = new Follow
        {
            FollowerId = user.UserId,
            FolloweeId = followeeID,
            FollowedAt = DateTime.UtcNow
        };

        _dbContext.Follows.Add(follow);
        await _dbContext.SaveChangesAsync();

        //Check if the follow was successful
        var followers = await _dbContext.Follows
            .Where(f => f.FollowerId == user.UserId)
            .Select(f => f.FolloweeId)
            .ToListAsync();
        if (followers.Contains(followeeID))
        {
            return "successfully followed";
        }
        else
        {
            return "failure to follow user";
        }
    }

    public async Task<String> UnfollowUser(User user, int followeeID)
    {
        Console.WriteLine("UserId is = " + user.UserId);
        Console.WriteLine("FollowerId is = " + followeeID);
        var follow = await _dbContext.Follows.FirstOrDefaultAsync(f => f.FollowerId == user.UserId && f.FolloweeId == followeeID);

        if (follow == null)
    {
        // If no follow relationship exists, return failure
        return "failure to unfollow user";
    }
        _dbContext.Remove(follow);
        await _dbContext.SaveChangesAsync();

        //Check if the Unfollow was successful
            var followers = await _dbContext.Follows
            .Where(f => f.FollowerId == user.UserId)
            .Select(f => f.FolloweeId)
            .ToListAsync();

        if (!followers.Contains(followeeID))
        {
            return "successfully unfollowed";
        }
        else
        {
            return "failure to unfollow user";
        }
    }
}