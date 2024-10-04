using SocialNetwork.Entities;

namespace SocialNetwork.Mappers.Responses;
public record LikeResponse(Guid id, Guid PostId, Guid UserId, DateTime CreatedAt)
{
    public static LikeResponse FromDomain(Like like)
    {
        return new LikeResponse(like.Id, like.PostId, like.UserId, like.CreatedAt);
    }
}
