using Chirp.Domain;

namespace MessageRepository;

public interface IMessageRepository{
    int GetCheepCount();

    //CreateMessage()

    Task<List<MessageDTO>> ReadMessages(int page);

    //AlterMessage()

}
