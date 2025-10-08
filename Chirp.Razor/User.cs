namespace Chirp.Razor;

public class User
{
    public int UserId {get; set;}
    public string name {get; set;}
    public ICollection<Message> messages {get; set;} 
}