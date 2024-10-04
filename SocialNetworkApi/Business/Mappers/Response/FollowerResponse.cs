namespace SocialNetworkApi.Business.Mappers.Response;

public class FollowerResponse
{
    public Guid FollowerId { get; set; }    
    public string FollowerName { get; set; } 
    public DateTime FollowedAt { get; set; } 

    public static FollowerResponse FromDomain(Guid followerId, string followerName, DateTime followedAt)
    {
        return new FollowerResponse
        {
            FollowerId = followerId,
            FollowerName = followerName,
            FollowedAt = followedAt
        };
    }
}
