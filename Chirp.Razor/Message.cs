namespace Chirp.Razor;

public class Message
{
    public int MessageId { get; set; }
    public string UserId { get; set; }
    public string text { get; set; }
    public User User { get; set; }
}