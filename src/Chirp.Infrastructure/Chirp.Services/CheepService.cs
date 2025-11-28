using Chirp.Domain;
namespace Chirp.Infrastructure;

// changed to interface for making libskob principle availible (for tests)
// changed to primary constructor as well :^)
public class CheepService(ICheepRepository cheepRepo, IUserRepository userRepo) : ICheepService
{

    public async Task<List<CheepDTO>> GetCheepsAsync(int page)
    {
        return await cheepRepo.ReadCheeps(page) ?? new List<CheepDTO>();
    }

    public async Task<List<CheepDTO>> GetCheepsFromUserId(string userId)
    {
        return await cheepRepo.getCheepsFromUserId(userId) ?? new List<CheepDTO>();
    }

    public async Task InsertCheepAsync(CheepDTO cheep)
    {
        await cheepRepo.InsertNewCheepAsync(cheep);
    }

    public async Task<User?> findUserByEmail(string email)
    {
        return await userRepo.findUserByEmail(email);
    }

    public async Task<List<CheepDTO>> getCheepsFromUser(User user, int page)
    {
        return await cheepRepo.getCheepsFromUser(user, page);
    }

    public async Task<List<User>> getFollowers(User user)
    {
        return await userRepo.getFollowers(user);
    }

    public async Task<List<string>> getFollowings(User user)
    {
        return await userRepo.getFollowings(user);
    }

    public async Task<String> followUser(User user, string followeeID)
    {
        return await userRepo.followUser(user, followeeID);
    }
    
    public async Task<String> UnfollowUser(User user, string followeeID)
    {
        return await userRepo.UnfollowUser(user, followeeID);
    }
}

