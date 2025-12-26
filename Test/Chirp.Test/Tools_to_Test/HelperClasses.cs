using Chirp.Domain;

namespace Chirp.Tests.Tools_to_Test;

public class HelperClasses
{
    public static User createRandomUser()
    {
        var name = InputFuzzers.RandomString(100);
        var user = new User
        {
            Id = Guid.NewGuid().ToString(),
            Name = name,
            Email = $"{name}@example.com",
            Cheeps = new List<Cheep>(),
            
        };
        return user;
    }

    public static Cheep createRandomCheep(User user)
    {
        var cheep = new Cheep
        {
            Text = InputFuzzers.RandomSentence(),
            User = user,
            TimeStamp = DateTime.UtcNow,
            UserId = user.Id,
        };
        return cheep;
    }
    public static CheepDTO createRandomCheepDTO(User user)
    {
        var cheep = new CheepDTO
        {
            Text = InputFuzzers.RandomSentence(),
            User = user,
            TimeStamp = DateTime.UtcNow,
        };
        return cheep;
    }
}