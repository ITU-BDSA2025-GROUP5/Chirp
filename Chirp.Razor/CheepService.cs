using Chirp.Razor.Data;
namespace Chirp.Razor;
public record CheepViewModel(string Author, string Message, string Timestamp);

public interface ICheepService
{
    public List<CheepViewModel> GetCheeps();
    public List<CheepViewModel> GetCheepsFromAuthor(string author);
    public int GetCheepCount();
}
public class CheepService: ICheepService
{
    private readonly IDbFacade _db;
    public CheepService(IDbFacade db) => _db = db;
    public int GetCheepCount() => _db.GetCheepCount();
    public List<CheepViewModel> GetCheeps() => _db.GetCheeps();
    public List<CheepViewModel> GetCheepsFromAuthor(string author) => _db.GetCheepsFromAuthor(author);
}
