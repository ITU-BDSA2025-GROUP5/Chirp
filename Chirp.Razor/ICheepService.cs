namespace Chirp.Razor;

public interface ICheepService
{
    Task<List<MessageDTO>> GetCheepsAsync();
}