namespace Chirp.Domain;

public class ApplicationUser : Microsoft.AspNetCore.Identity.applicationUser
{
    public User? DomainUser { get; set; }
}
