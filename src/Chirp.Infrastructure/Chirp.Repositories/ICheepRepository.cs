using Chirp.Domain;

namespace Chirp.Infrastructure;

public interface ICheepRepository
{

    Task<int> GetCheepCount();

    Task<List<CheepDTO>> ReadCheeps(int page);

    Task<User?> findAuthorByName(string name);

    Task<User?> findAuthorByEmail(string email);

    Task InsertNewCheepAsync(CheepDTO message);
    
    Task<List<CheepDTO>> getCheepsFromUser(User user, int page);
    
    Task<List<User>> getFollowers(User user);
    
    Task<List<User>> getFollowings(User user);

}
