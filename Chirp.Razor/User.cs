namespace Chirp.Razor;

public class User
{
    public int UserId {get; set;}
    public required string name {get; set;}
    public required ICollection<Message> messages {get; set;} 
}