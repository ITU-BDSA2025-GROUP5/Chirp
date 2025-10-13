namespace Chirp.Razor;

public class Message
{
    public int MessageId { get; set; }
    public required string UserId { get; set; }
    public required string Text { get; set; }
    public required User User { get; set; }
}