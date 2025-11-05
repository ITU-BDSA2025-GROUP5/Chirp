namespace Chirp.Domain;

public class ApplicationUser : Microsoft.AspNetCore.Identity.IdentityUser
{
    public User? DomainUser { get; set; }
}
