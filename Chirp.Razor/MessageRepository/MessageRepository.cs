namespace Chirp.Razor.MessageRepository;

public class MessageRepository : IMessageRepository
{
    private readonly CheepDbContext _dbContext;
    public MessageRepository(CheepDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public int GetCheepCount() 
    {
        return 5;
    }

    public async Task<List<CheepViewModel>> ReadMessages()
    {
        List<CheepViewModel> Cheeps = new List<CheepViewModel>();
        var blogs = _dbContext.Messages.ToList();
        //Cheeps = _dbContext.Messages.Select(message => new { message.User, message.Text });

        return Cheeps;
    }
}