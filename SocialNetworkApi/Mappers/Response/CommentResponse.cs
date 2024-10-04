using SocialNetworkApi.DataAccess.Entities;

namespace SocialNetworkApi.Mappers.Response;

public record CommentResponse(Guid CommentId, Guid UserId, string Content, DateTime CreatedAt)
{
    public static CommentResponse FromDomain(Comment comment)
    {
        return new CommentResponse(comment.Id, comment.UserId, comment.Content, comment.CreatedAt);
    }
}
