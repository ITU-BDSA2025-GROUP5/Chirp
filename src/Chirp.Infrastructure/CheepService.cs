using Chirp.Domain;
namespace Chirp.Infrastructure;

public interface ICheepService
{
   Task<List<MessageDTO>> GetCheepsAsync(int page);
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
}

