using Chirp.Domain;
namespace Chirp.Infrastructure;

public interface ICheepService
{
    Task<List<CheepDTO>> GetCheepsAsync(int page);
    Task InsertCheepAsync(CheepDTO cheep);

    Task<User?> findUserByEmail(string email);

    Task<List<CheepDTO>> getCheepsFromUser(User user, int page);

    Task<List<User>> getFollowers(User user);

    Task<List<string>> getFollowings(User user);
    Task<String> followUser(User userid, string followeeID);
    Task<String> UnfollowUser(User userid, string followeeID);
    Task<List<CheepDTO>> GetCheepsFromUserId(string UserId);
}