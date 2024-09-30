using SocialNetwork.Domain;

namespace SocialNetwork.Mappers.Requests;
public class EditPostRequest
    {
    public required string Content { get; set; }
        public required string Image { get; set; }

    public Post ToDomain()
        {
            return new Post
            {
                Content = this.Content,
                Image = this.Image
            };
        }
    }
