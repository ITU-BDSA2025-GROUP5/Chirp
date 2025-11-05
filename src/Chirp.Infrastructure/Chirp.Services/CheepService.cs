using Chirp.Domain;
namespace Chirp.Infrastructure;

public class CheepService : ICheepService
{
    private readonly CheepRepo _cheepRepo;
    public CheepService(CheepRepo cheepRepo)
    {
        _cheepRepo = cheepRepo;
    }

    public async Task<List<CheepDTO>> GetCheepsAsync(int page)
    {
        return await _cheepRepo.ReadCheeps(page) ?? new List<CheepDTO>();
    }
    
        public async Task InsertCheepAsync(CheepDTO cheep)
    {
        await _cheepRepo.InsertNewCheepAsync(cheep);
    }
        
    public async Task<User?> findAuthorByEmail(string email)
          {
              return await _cheepRepo.findAuthorByEmail(email);
          }
    

}

