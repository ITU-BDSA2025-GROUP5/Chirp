using Chirp.Domain;
namespace Chirp.Infrastructure;

public interface ICheepService
{
    Task<List<MessageDTO>> GetCheepsAsync(int page);
    Task InsertCheepAsync(MessageDTO cheep);
}
public class CheepService : ICheepService
{
    private readonly MessageRepo _messageRepo;
    public CheepService(MessageRepo messageRepo)
    {
        _messageRepo = messageRepo;
    }

    public async Task<List<MessageDTO>> GetCheepsAsync(int page)
    {
        return await _messageRepo.ReadMessages(page) ?? new List<MessageDTO>();
    }

    public async Task InsertCheepAsync(MessageDTO cheep)
{
    await _messageRepo.InsertNewCheepAsync(cheep);
}
}

