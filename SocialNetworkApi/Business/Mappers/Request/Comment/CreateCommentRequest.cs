namespace SocialNetworkApi.Business.Mappers.Request.Comment;

public record CreateCommentRequest(Guid UserId, string Content)
{
    public DataAccess.Entities.Comment ToDomain(Guid postId)
    {
        return new DataAccess.Entities.Comment
        {
            PostId = postId,
            UserId = UserId,
            Content = Content
        };
    }
}
