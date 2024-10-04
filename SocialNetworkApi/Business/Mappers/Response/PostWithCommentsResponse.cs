using SocialNetworkApi.DataAccess.Entities;

namespace SocialNetworkApi.Business.Mappers.Response;

public record PostWithCommentsResponse(Guid PostId, Guid UserId, string Content, string ImageUrl, DateTime CreatedAt, IEnumerable<CommentResponse> Comments)
{
    public static PostWithCommentsResponse FromDomain(Post post, IEnumerable<Comment> comments)
    {
        var commentResponses = comments.Select(CommentResponse.FromDomain);
        return new PostWithCommentsResponse(post.Id, post.UserId, post.Content, post.ImageUrl, post.CreatedAt, commentResponses);
    }
}
