namespace Chirp.Domain;

public class Follow
{
    public required string FollowerId { get; set; }
    public required string FolloweeId { get; set; }
    public required DateTime FollowedAt { get; set; }
    public User Follower { get; set; } = null!;
    public User Followee { get; set; } = null!;
}