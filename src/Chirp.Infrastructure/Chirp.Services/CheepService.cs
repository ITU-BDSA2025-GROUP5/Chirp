using Chirp.Domain;
namespace Chirp.Infrastructure;

public class CheepService : ICheepService
{
    private readonly CheepRepo _cheepRepo;
    private readonly UserRepository _userRepo;
    public CheepService(CheepRepo cheepRepo, UserRepository userRepo)
    {
        _cheepRepo = cheepRepo;
        _userRepo = userRepo;
    }

    public async Task<List<CheepDTO>> GetCheepsAsync(int page)
    {
        return await _cheepRepo.ReadCheeps(page) ?? new List<CheepDTO>();
    }

    public async Task<List<CheepDTO>> GetCheepsFromUserId(int userId)
    {
        return await _cheepRepo.getCheepsFromUserId(userId) ?? new List<CheepDTO>();
    }

    public async Task InsertCheepAsync(CheepDTO cheep)
    {
        await _cheepRepo.InsertNewCheepAsync(cheep);
    }

    public async Task<User?> findUserByEmail(string email)
    {
        return await _userRepo.findUserByEmail(email);
    }

    public async Task<List<CheepDTO>> getCheepsFromUser(User user, int page)
    {
        return await _cheepRepo.getCheepsFromUser(user, page);
    }

    public async Task<List<User>> getFollowers(User user)
    {
        return await _userRepo.getFollowers(user);
    }

    public async Task<List<int>> getFollowings(User user)
    {
        return await _userRepo.getFollowings(user);
    }

    public async Task<String> followUser(User user, int followeeID)
    {
        return await _userRepo.followUser(user, followeeID);
    }
    
    public async Task<String> UnfollowUser(User user, int followeeID)
    {
        return await _userRepo.UnfollowUser(user, followeeID);
    }
}

