using Chirp.Domain;
using Chirp.Infrastructure;

public class CheepRepositoryFake : ICheepRepository
{
    // to isolate the CheepRepository 
    private readonly List<CheepDTO> _cheeps = new();

    public Task<int> GetCheepCount()
    {
        return Task.FromResult(_cheeps.Count);
    }

    public Task<List<CheepDTO>> ReadCheeps(int page)
    {
        return Task.FromResult(_cheeps.ToList());
    }

    public Task InsertNewCheepAsync(CheepDTO message)
    {
        _cheeps.Add(message);
        return Task.CompletedTask;
    }

    public Task<List<CheepDTO>> getCheepsFromUser(User user, int page)
    {
        var cheeps = _cheeps.Where(c => c.User == user).ToList();
        return Task.FromResult(cheeps);
    }

    public Task<List<CheepDTO>?> getCheepsFromUserId(string userId)
    {
        var cheeps = _cheeps.Where(c => c.User.Id == userId).ToList();
        return Task.FromResult<List<CheepDTO>?>(cheeps);
    }

    public Task<string> LikeCheep(User currentUser, int cheepId)
    {
        throw new NotImplementedException();
    }

    public Task<string> UnLikeCheep(User currentUser, int cheepId)
    {
        throw new NotImplementedException();
    }
}