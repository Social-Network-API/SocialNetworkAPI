namespace SocialNetworkApi.Business.Mappers.Request.Comment;

public record UpdateCommentResponse(string Content)
{
    public DataAccess.Entities.Comment ToDomain()
    {
        return new DataAccess.Entities.Comment
        {
            Content = Content,
            CreatedAt = DateTime.UtcNow
        };
    }
}
