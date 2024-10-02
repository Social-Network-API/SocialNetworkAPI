using SocialNetwork.Domain;
using SocialNetwork.Persistence.DataBase;

namespace SocialNetwork.Mappers.Requests
{
    public record CreatePostRequest(string Content, string ImageUrl)
    {
        public Post ToDomain()
        {
            return new Post
            {
                Id = Guid.NewGuid(),
                Content = Content,
                ImageUrl = ImageUrl,
                CreatedAt = DateTime.UtcNow,
                UserId =UserContext.CurrentUserId
            };
        }
    }
}
