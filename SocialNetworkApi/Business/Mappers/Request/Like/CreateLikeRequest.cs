namespace SocialNetworkApi.Business.Mappers.Request.Like;

public record CreateLikeRequest(Guid UserId)
{
    public DataAccess.Entities.Like ToDomain()
    {
        return new DataAccess.Entities.Like
        {
            Id = Guid.NewGuid(),
            UserId = UserId,
            CreatedAt = DateTime.UtcNow,
        };
    }
}
