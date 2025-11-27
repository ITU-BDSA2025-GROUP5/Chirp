using Chirp.Domain;

namespace Chirp.Infrastructure;

public interface ICheepRepository
{

    Task<int> GetCheepCount();

    Task<List<CheepDTO>> ReadCheeps(int page);

    Task InsertNewCheepAsync(CheepDTO message);

    Task<List<CheepDTO>> getCheepsFromUser(User user, int page);

    Task<List<CheepDTO>?> getCheepsFromUserId(string userId);
    
}
