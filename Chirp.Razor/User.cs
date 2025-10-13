namespace Chirp.Razor;

public class User
{
    public int UserId {get; set;}
    public required string Name {get; set;}
    public required ICollection<Message> Messages {get; set;} 
}