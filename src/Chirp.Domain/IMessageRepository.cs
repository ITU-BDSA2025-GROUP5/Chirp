using Chirp.Domain;

namespace MessageRepository;

public interface IMessageRepository{
    int GetCheepCount();

    Task InsertNewCheepAsync(MessageDTO Cheep);

    Task<List<MessageDTO>> ReadMessages(int page);

    //AlterMessage()

}
