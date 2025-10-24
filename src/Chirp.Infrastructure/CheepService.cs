using Chirp.Razor.MessageRepository;
namespace Chirp.Razor;

public interface ICheepService
{
   Task<List<MessageDTO>> GetCheepsAsync();
}
public class CheepService : ICheepService
{
    private readonly MessageRepo _messageRepo;
    public CheepService(MessageRepo messageRepo)
    {
        _messageRepo = messageRepo;
    }

    public async Task<List<MessageDTO>> GetCheepsAsync()
    {
        return await _messageRepo.ReadMessages() ?? new List<MessageDTO>();
    }
}

