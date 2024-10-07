namespace SocialNetworkApi.DataAccess.Entities;

public class Follower
{
    public Guid Id { get; set; }
    public Guid FollowerId { get; set; }  
    public Guid FollowedId { get; set; }  
    public DateTime CreatedAt { get; set; }  

    public Follower()
    {
        Id = Guid.NewGuid();
        CreatedAt = DateTime.UtcNow;
    }
}
