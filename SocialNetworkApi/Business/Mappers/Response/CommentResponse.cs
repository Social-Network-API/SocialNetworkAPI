using SocialNetwork.Entities;

namespace SocialNetwork.Mappers.Responses;
public record CommentResponse(Guid CommentId, Guid PostId, Guid UserId, string Content, DateTime CreatedAt)
{
    public static CommentResponse FromDomain(Comment comment)
    {
        return new CommentResponse(comment.Id, comment.PostId, comment.UserId, comment.Content, comment.CreatedAt);
    }
}
