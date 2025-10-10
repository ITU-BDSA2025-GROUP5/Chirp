using Chirp.Razor.Data;
namespace Chirp.Razor;
public record CheepViewModel(string Author, string Message, string Timestamp);

public interface ICheepService
{
    List<CheepViewModel> getCheeps();
}
public class CheepService : ICheepService
{
    public List<CheepViewModel> getCheeps()
    {
        List<CheepViewModel> Cheeps = new List <CheepViewModel>();
        
        return Cheeps;
    }
}

