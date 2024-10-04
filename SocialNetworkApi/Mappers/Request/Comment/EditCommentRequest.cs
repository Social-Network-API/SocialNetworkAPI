namespace SocialNetworkApi.Mappers.Request.Comment;

public record EditCommentRequest(string Content)
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

