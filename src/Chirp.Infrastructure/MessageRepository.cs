using Chirp.Domain;

using Microsoft.EntityFrameworkCore;

namespace Infrastructure;

public class MessageRepo : IMessageRepository
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
            var Cheeps = await _dbContext.Cheeps
            .AsNoTracking()
            .Select(m => new MessageDTO
            {
                Text = m.Text,
                User = m.User,  
                TimeStamp = m.TimeStamp
            })
            .ToListAsync();
        return Cheeps;
    }
}