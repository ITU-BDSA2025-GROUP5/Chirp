namespace Chirp.Razor.Data;

public interface IDbFacade
{
    int GetCheepCount();
    List<CheepViewModel> GetCheeps();
    List<CheepViewModel> GetCheepsFromAuthor(string author);
}