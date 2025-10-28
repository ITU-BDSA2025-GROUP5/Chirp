namespace Chirp.Domain;

public class User
{
    public int UserId {get; set;}
    public required string Name {get; set;}
    public required string Email{ get; set; }
    public required ICollection<Cheep> Cheeps { get; set; } 
}