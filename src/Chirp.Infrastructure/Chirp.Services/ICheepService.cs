using Chirp.Domain;
namespace Chirp.Infrastructure;

public interface ICheepService
{
    Task<List<MessageDTO>> GetCheepsAsync(int page);
}