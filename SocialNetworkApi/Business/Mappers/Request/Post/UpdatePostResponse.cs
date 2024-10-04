namespace SocialNetworkApi.Business.Mappers.Request.Post;

public record UpdatePostResponse(string Content, string ImageUrl)
{
    public DataAccess.Entities.Post ToDomain()
    {
        return new DataAccess.Entities.Post
        {
            Content = Content,
            ImageUrl = ImageUrl
        };
    }
}