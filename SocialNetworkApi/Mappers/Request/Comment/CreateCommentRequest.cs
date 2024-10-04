namespace SocialNetworkApi.Mappers.Request.Comment;

public record CreateCommentRequest(Guid UserId, string Content)
{
    public DataAccess.Entities.Comment ToDomain(Guid PostId)
    {
        return new DataAccess.Entities.Comment
        {
            PostId = PostId,
            UserId = UserId,
            Content = Content
        };
    }
}
