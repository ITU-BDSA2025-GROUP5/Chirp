using Chirp.Domain;

namespace Chirp.Application;

public interface ICheepRepository{
    int GetCheepCount();

    //CreateMessage()

    Task<List<MessageDTO>> ReadMessages(int page);

    //AlterMessage()

}
