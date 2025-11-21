namespace Chirp.Domain;


public class User : Microsoft.AspNetCore.Identity.IdentityUser
{
    public string? Name { get; set; }
    public required ICollection<Cheep> Cheeps { get; set; }
    
}