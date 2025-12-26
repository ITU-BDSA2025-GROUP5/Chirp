using Chirp.Domain;
namespace Chirp.Infrastructure;

// changed to interface for making libskob principle availible (for tests)
// changed to primary constructor as well :^)
public class CheepService(ICheepRepository cheepRepo, IUserService userService) : ICheepService
{

    public async Task<List<CheepDTO>> GetCheepsAsync(int page)
    {
        return await cheepRepo.ReadCheeps(page) ?? new List<CheepDTO>();
    }

    public async Task InsertCheepAsync(CheepDTO cheep)
    {
        await cheepRepo.InsertNewCheepAsync(cheep);
    }

    public async Task<User?> findUserByEmail(string email)
    {
        return await userService.FindByEmailAsync(email);
    }

    public async Task<User?> findUserByName(string name)
    {
        return await userService.FindByNameAsync(name);
    }

    public async Task<List<CheepDTO>> getCheepsFromUser(User user, int page)
    {
        return await cheepRepo.getCheepsFromUser(user, page);
    }

    public async Task<List<User>> getFollowers(User user)
    {
        return await userService.GetFollowersAsync(user);
    }

    public async Task<List<string>> getFollowings(User user)
    {
        return await userService.GetFollowingsAsync(user);
    }

    public async Task<String> followUser(User user, string followeeID)
    {
        return await userService.FollowAsync(user, followeeID);
    }

    public async Task<String> UnfollowUser(User user, string followeeID)
    {
        return await userService.UnfollowAsync(user, followeeID);
    }

    public Task<User?> FindTimelineByUserNameAsync(string userName)
    {
        throw new NotImplementedException();
    }

    public async Task<List<CheepDTO>> GetCheepsFromUserId(string userId)
    {
        return await cheepRepo.getCheepsFromUserId(userId) ?? new List<CheepDTO>();
    }

    public async Task<string> LikeCheep(User currentUser, int cheepId)
    {
        return await cheepRepo.LikeCheep(currentUser, cheepId);
    }
    
    public async Task<string> UnLikeCheep(User currentUser, int cheepId)
    {
        return await cheepRepo.UnLikeCheep(currentUser,cheepId);
    }
}

