namespace Chirp.Domain;

public class Follow
{
    public required int FollowerId { get; set; }
    public required int FolloweeId { get; set; }
    public required DateTime follewedAt { get; set; }
    public User Follower { get; set; } = null!;
    public User Followee { get; set; } = null!;
}