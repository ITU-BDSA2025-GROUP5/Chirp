namespace Chirp.CLI;



public record Cheep
{
    public required string Author { get; set; }
    public required string Message { get; set; }
    public required long Timestamp { get; set; }
}
