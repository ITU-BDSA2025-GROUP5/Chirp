namespace Chirp.Razor.MessageRepository;

public interface IMessageRepository{
    int GetCheepCount();

    //CreateMessage()

    Task<List<MessageDTO>> ReadMessages();

    //AlterMessage()

}
