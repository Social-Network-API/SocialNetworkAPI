namespace SocialNetworkApi.Business.Mappers.Request.Like;

public record UpdateLikeResponse(Guid UserId, Guid PostId)
{
    public DataAccess.Entities.Like ToDomain()
    {
        return new DataAccess.Entities.Like
        {
            UserId = UserId,
            PostId = PostId,
            CreatedAt = DateTime.UtcNow,
        };
    }
}