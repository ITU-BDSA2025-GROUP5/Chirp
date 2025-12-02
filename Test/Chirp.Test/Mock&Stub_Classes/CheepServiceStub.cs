using Chirp.Domain;
using Chirp.Infrastructure;

namespace Chirp.Tests.Mock_Stub_Classes;

public class CheepServiceStub : ICheepService
{
    public Dictionary<string, User> Users = new();
    public Dictionary<string, List<string>> Followings = new();

    public Task<List<CheepDTO>> GetCheepsAsync(int page)
        => Task.FromResult(new List<CheepDTO>());

    public Task<List<CheepDTO>> GetCheepsFromUserId(string userId)
        => Task.FromResult(new List<CheepDTO>());

    public Task<string> LikeCheep(User currentUser, int cheepId)
    {
        throw new NotImplementedException();
    }

    public Task<string> UnLikeCheep(User currentUser, int cheepId)
    {
        throw new NotImplementedException();
    }

    public Task InsertCheepAsync(CheepDTO cheep)
        => Task.CompletedTask;

    public Task<User?> findUserByEmail(string email)
    {
        var user = Users.Values.FirstOrDefault(u => u.Email == email);
        return Task.FromResult(user);
    }

    public Task<User?> findUserByName(string name)
    {
        throw new NotImplementedException();
    }

    public Task<List<CheepDTO>> getCheepsFromUser(User user, int page)
        => Task.FromResult(new List<CheepDTO>());

    public Task<User?> FindTimelineByUserNameAsync(string userName)
    {
        throw new NotImplementedException();
    }

    public Task<List<User>> getFollowers(User user)
    {
        var followers = Users.Values
            .Where(u => Followings.ContainsKey(u.Id) &&
                        Followings[u.Id].Contains(user.Id))
            .ToList();

        return Task.FromResult(followers);
    }

    public Task<List<string>> getFollowings(User user)
    {
        if (Followings.TryGetValue(user.Id, out var follows))
            return Task.FromResult(follows);

        return Task.FromResult(new List<string>());
    }

    public Task<string> followUser(User user, string followeeId)
    {
        if (!Followings.ContainsKey(user.Id))
            Followings[user.Id] = new List<string>();

        Followings[user.Id].Add(followeeId);
        return Task.FromResult("ok");
    }

    public Task<string> UnfollowUser(User user, string followeeId)
    {
        if (Followings.TryGetValue(user.Id, out var follows))
            follows.Remove(followeeId);

        return Task.FromResult("ok");
    }
}
