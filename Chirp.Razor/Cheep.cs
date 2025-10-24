using System.ComponentModel.DataAnnotations;

namespace Chirp.Razor;

public class Cheep
{
    public int CheepId { get; set; }
    public required int UserId { get; set; } 
    
    [StringLength(160)]
    public required string Text { get; set; }
    
    public required User User { get; set; }
    public DateTime TimeStamp { get; set; }
}