using Chirp.Domain;

namespace Chirp.Infrastructure;

public interface ICheepRepository{
    int GetCheepCount();

    //CreateMessage()

    Task<List<MessageDTO>> ReadCheeps(int page);

    //AlterMessage()

}
