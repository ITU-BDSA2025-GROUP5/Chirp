namespace Chirp.Razor.MessageRepository;

public interface IMessageRepository{
    int GetCheepCount();

    //CreateMessage()

    Task<List<CheepViewModel>> ReadMessages();

    //AlterMessage()

}
