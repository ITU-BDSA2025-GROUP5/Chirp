namespace Chirp.Domain;

public class CheepDTO
{
    public required string Text { get; set; }
    public required string UserId { get; set; }

    public string? UserName {get; set;}

    public DateTime TimeStamp { get; set; }
}
