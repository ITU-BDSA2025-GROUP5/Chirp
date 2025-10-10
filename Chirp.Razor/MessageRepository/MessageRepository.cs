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

    public async Task<List<MessageDTO>> ReadMessages(string userName){

        var query = _dbContext.Messages.Select(message => new { message.User, message.Text });

    }





}