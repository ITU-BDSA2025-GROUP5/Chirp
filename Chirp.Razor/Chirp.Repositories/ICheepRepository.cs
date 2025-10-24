namespace Chirp.Razor.Chirp.Repositories;

public interface ICheepRepository{
    int GetCheepCount();

    //CreateMessage()

    Task<List<MessageDTO>> ReadMessages();

    Task<User?> findAuthorByName(string name);

    Task<User?> findAuthorByEmail(string email);
    
    void createNewAuthor(string name, string email);
    
    
    
    //AlterMessage()

}
