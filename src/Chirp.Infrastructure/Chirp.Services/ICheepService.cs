using Chirp.Domain;
namespace Chirp.Infrastructure;

public interface ICheepService
{
    Task<List<CheepDTO>> GetCheepsAsync(int page);
}