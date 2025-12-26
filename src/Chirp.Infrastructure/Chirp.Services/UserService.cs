namespace Chirp.Infrastructure;

using Chirp.Domain;
using Microsoft.AspNetCore.Identity;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepo;
    private readonly UserManager<User> _userManager;

    public UserService(IUserRepository users, UserManager<User> userManager)
    {
        _userRepo = users;
        _userManager = userManager;
    }

    public Task<User?> FindByIdAsync(string id)
        => _userManager.FindByIdAsync(id);

    public Task<User?> FindByNameAsync(string name)
        => _userRepo.findUserByName(name); // or _userManager.FindByNameAsync(name) if that’s canonical

    public Task<User?> FindByEmailAsync(string email) => _userRepo.findUserByEmail(email); // or _userManager.FindByEmailAsync(email)

    public async Task<string> FollowAsync(User follower, string followeeId)
    {
        if (string.IsNullOrWhiteSpace(followeeId)) return "invalid-followee";
        if (follower.Id == followeeId) return "cannot-follow-self";

        var followee = await FindByIdAsync(followeeId);
        if (followee is null) return "followee-not-found";

        // Optional: prevent duplicates by checking existing followings
        var current = await _userRepo.getFollowings(follower);
        if (current.Contains(followeeId)) return "already-following";

        return await _userRepo.followUser(follower, followeeId);
    }

    public Task<string> UnfollowAsync(User follower, string followeeId)
        => _userRepo.UnfollowUser(follower, followeeId);

    public Task<List<User>> GetFollowersAsync(User user)
        => _userRepo.getFollowers(user);

    public Task<List<string>> GetFollowingsAsync(User user)
        => _userRepo.getFollowings(user);
}
