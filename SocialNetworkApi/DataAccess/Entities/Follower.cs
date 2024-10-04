namespace SocialNetworkApi.DataAccess.Entities;

public class Follower
{
    public Guid Id { get; set; }
    public Guid FollowerId { get; set; }  // El usuario que sigue
    public Guid FollowedId { get; set; }  // El usuario que es seguido
    public DateTime CreatedAt { get; set; }  // Cuándo se inició el seguimiento

    public Follower()
    {
        Id = Guid.NewGuid();
        CreatedAt = DateTime.UtcNow;
    }
}
