using Chirp.Domain;
using MessageRepository;
using Microsoft.EntityFrameworkCore;

namespace Chirp.Infrastructure;

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