using SocialNetworkApi.DataAccess.Entities;

namespace SocialNetworkApi.Business.Mappers.Response;
public record LikeResponse(Guid id, Guid PostId, Guid UserId, DateTime CreatedAt)
{
    public static LikeResponse FromDomain(Like like)
    {
        return new LikeResponse(like.Id, like.PostId, like.UserId, like.CreatedAt);
    }
}
