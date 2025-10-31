using Chirp.Domain;
using Microsoft.EntityFrameworkCore;

namespace Chirp.Infrastructure;

public class CheepRepo : ICheepRepository
{
    private readonly CheepDbContext _dbContext;

    public CheepRepo(CheepDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<int> GetCheepCount()
    {
        return await _dbContext.Cheeps.AsNoTracking().CountAsync();
    }

    public async Task<List<CheepDTO>> ReadCheeps(int page)
    {
        const int pageSize = 32;
        var skip = Math.Max(0, (page - 1) * pageSize);
        
        var cheeps = await _dbContext.Cheeps
        .AsNoTracking()
        .OrderByDescending(c => c.TimeStamp)
        .Include(c => c.User)               
        .Skip(skip)
        .Take(pageSize)
        .Select(c => new CheepDTO
        {
            Text = c.Text,
            User = c.User,
            TimeStamp = c.TimeStamp
        })
        .ToListAsync();
        return cheeps;
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
    public async Task InsertNewCheepAsync(CheepDTO message)
    {
        var newCheep = new Cheep
        {
            UserId = 0,
            Text = message.Text,
            User = message.User,
            TimeStamp = message.TimeStamp,
        };
        _dbContext.Cheeps.Add(newCheep);
        await _dbContext.SaveChangesAsync(); 
    }
}