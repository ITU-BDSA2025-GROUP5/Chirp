using Microsoft.EntityFrameworkCore;

namespace Chirp.Razor.MessageRepository;

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

    public Task<List<CheepViewModel>> ReadMessages()
    {
        Task<List<CheepViewModel>> Cheeps = _dbContext.Messages.Select(m => new CheepViewModel(
                m.User.Name,
                m.Text)).AsNoTracking().ToListAsync();
        //Cheeps = _dbContext.Messages.Select(message => new { message.User, message.Text });

        return Cheeps;
    }
}