using SocialNetworkApi.DataAccess.Entities;

namespace SocialNetworkApi.Mappers.Response;

public record PostResponse(Guid Id, Guid UserId, string Content, string ImageUrl, DateTime CreatedAt)
{
    public static PostResponse FromDomain(Post post)
    {
        return new PostResponse(post.Id, post.UserId, post.Content, post.ImageUrl, post.CreatedAt);
    }
}
