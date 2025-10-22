using Chirp.Razor;
public class MessageDTO
{
    public required string Text { get; set; }
    public required User User { get; set; }
    public DateTime TimeStamp { get; set; }
}