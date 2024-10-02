using SocialNetwork.Domain;

namespace SocialNetwork.Mappers.Requests
{
    public record EditCommentRequest(string Content)
    {
        public Comment ToDomain()
        {
            return new Comment
            {
                Content = Content,
                CreatedAt = DateTime.UtcNow
            };
        }
    }
}
