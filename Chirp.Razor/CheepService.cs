using Chirp.Razor.Chirp.Repositories;
namespace Chirp.Razor;

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

