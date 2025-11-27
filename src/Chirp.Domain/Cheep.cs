using System.ComponentModel.DataAnnotations;
namespace Chirp.Domain;

public class Cheep
{
    public int CheepId { get; set; }
    
    [StringLength(160)]
    public required string Text { get; set; }
    public User User { get; set; }
    public required string UserId { get; set; }
    public DateTime TimeStamp { get; set; }
}
