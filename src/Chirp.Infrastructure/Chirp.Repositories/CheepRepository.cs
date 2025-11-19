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
    
    public async Task InsertNewCheepAsync(CheepDTO message)
    {
        var newCheep = new Cheep
        {
            Text = message.Text,
            User = message.User,
            TimeStamp = message.TimeStamp,
        };
        _dbContext.Cheeps.Add(newCheep);
        await _dbContext.SaveChangesAsync(); 
    }


    public async Task<List<CheepDTO>> getCheepsFromUser(User user, int page)
    {
        const int pageSize = 32;
        var skip = Math.Max(0, (page - 1) * pageSize);
        
        var cheeps = await _dbContext.Cheeps
            .AsNoTracking()
            .OrderByDescending(c => c.TimeStamp)
            .Include(c => c.User)               
            .Skip(skip)
            .Take(pageSize)
            .Where(c => c.User == user)
            .Select(c => new CheepDTO
            {
                Text = c.Text,
                User = c.User,
                TimeStamp = c.TimeStamp
            })
            .ToListAsync();
        return cheeps;
    }
    
    
}