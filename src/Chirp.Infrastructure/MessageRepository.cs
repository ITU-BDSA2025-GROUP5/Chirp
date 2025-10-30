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
        const int pageSize = 32;
        var skip = Math.Max(0, (page - 1) * pageSize);
        
        var cheeps = await _dbContext.Cheeps
        .AsNoTracking()
        .OrderByDescending(c => c.TimeStamp)
        .Include(c => c.User)               
        .Skip(skip)
        .Take(pageSize)
        .Select(c => new MessageDTO
        {
            Text = c.Text,
            User = c.User,
            TimeStamp = c.TimeStamp
        })
        .ToListAsync();
        return cheeps;
    }

    public async Task InsertNewCheepAsync(MessageDTO message)
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