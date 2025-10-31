using Chirp.Domain;
namespace Chirp.Infrastructure;

public interface ICheepService
{
    Task<List<CheepDTO>> GetCheepsAsync(int page);
    Task InsertCheepAsync(CheepDTO cheep);
    /*
    Task<User?> findAuthorByEmail(string email);
    */

}