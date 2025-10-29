using Chirp.Domain;
using Microsoft.EntityFrameworkCore;

namespace Chirp.Application;

public class CheepRepo : ICheepRepository
{
    private readonly CheepDbContext _dbContext;
    
    public CheepRepo(CheepDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public int GetCheepCount() 
    {
        return 5;
    }

    public async Task<List<MessageDTO>> ReadMessages(int page)
    {
        var offset = 32;
        offset = offset * page;
            var Cheeps = await _dbContext.Cheeps.AsNoTracking()
            .Select(m => new MessageDTO
            {
                Text = m.Text,
                User = m.User,  
                TimeStamp = m.TimeStamp
            }).Skip(offset).Take(32).ToListAsync();
        return Cheeps;
    }
}