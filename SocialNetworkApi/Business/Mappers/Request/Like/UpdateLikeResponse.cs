using SocialNetwork.Entities;

namespace SocialNetwork.Mappers.Requests;
public record UpdateLikeResponse(Guid UserId, Guid PostId)
{
    public Like ToDomain()
    {
        return new Like
        {
            UserId = UserId,
            PostId = PostId,
            CreatedAt = DateTime.UtcNow,
        };
    }
}