namespace Chirp.Razor.Data;

public interface IDbFacade
{
    int GetCheepCount();
    List<CheepViewModel> GetCheeps();
    List<CheepViewModel> GetCheepsFromAuthor(string author);
    string AddCheep(string username, string email , string pw_hash, int message_id, int author_id, string text, int pub_date);
}