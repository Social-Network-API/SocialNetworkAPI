namespace SocialNetworkApi.Business.Mappers.Response;

public class FollowingResponse
{
    public Guid FollowedId { get; set; }    
    public string FollowedName { get; set; } 
    public DateTime FollowedAt { get; set; } 

    public static FollowingResponse FromDomain(Guid followedId, string followedName, DateTime followedAt)
    {
        return new FollowingResponse
        {
            FollowedId = followedId,
            FollowedName = followedName,
            FollowedAt = followedAt
        };
    }
}
