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

    public static CheepDTO createRandomCheep(User user)
    {
        CheepDTO cheep = new CheepDTO
        {
            Text = InputFuzzers.RandomSentence(),
            User = user,
            TimeStamp = DateTime.UtcNow
        };
        return cheep;
    }
}