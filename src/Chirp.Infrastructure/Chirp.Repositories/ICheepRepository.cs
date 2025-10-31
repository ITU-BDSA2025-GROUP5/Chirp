using Chirp.Domain;

namespace Chirp.Infrastructure;

public interface ICheepRepository{
    
    Task<int> GetCheepCount();

    Task<List<CheepDTO>> ReadCheeps(int page);
    
    Task<User?> findAuthorByName(string name);

    Task<User?> findAuthorByEmail(string email);
    
    void createNewAuthor(string name, string email);

}
