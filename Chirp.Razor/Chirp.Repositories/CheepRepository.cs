using Microsoft.EntityFrameworkCore;

namespace Chirp.Razor.Chirp.Repositories;

public class MessageRepo : ICheepRepository
{
    private readonly CheepDbContext _dbContext;

    public MessageRepo(CheepDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public int GetCheepCount()
    {
        return 5;
    }

    public async Task<List<MessageDTO>> ReadMessages()
    {
        var Cheeps = await _dbContext.Cheeps.AsNoTracking().Select(m => new MessageDTO
            {
                Text = m.Text,
                User = m.User,
                TimeStamp = m.TimeStamp
            })
            .ToListAsync();
        return Cheeps;
    }

    public async Task<User?> findAuthorByName(string name)
    {
        return await _dbContext.Users.FirstOrDefaultAsync(u => u.Name == name);
    }
    
    public async Task<User?> findAuthorByEmail(string email)
    {
        return await _dbContext.Users.FirstOrDefaultAsync(u => u.Email == email);
    }

    public void createNewAuthor(string name, string email)
    {
        var user = new User
        {
            Name = name,
            Email = email,
            Cheeps = new List<Cheep>()
        };
        _dbContext.Users.Add(user);
    }

}