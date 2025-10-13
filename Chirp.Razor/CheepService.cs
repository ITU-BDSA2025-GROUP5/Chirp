using Chirp.Razor.MessageRepository;
namespace Chirp.Razor;
public record CheepViewModel(string Author, string Message);

public interface ICheepService
{
   List<CheepViewModel> getCheeps();
}
public class CheepService : ICheepService
{
    public CheepService(CheepDbContext db)
    {
        CheepDbContext _db = db;
    }
    private readonly MessageRepo _messageRepo;
    public List<CheepViewModel> getCheeps()
    {
        List<CheepViewModel> Cheeps = _messageRepo.ReadMessages().Result;
        return Cheeps;
    }
}

