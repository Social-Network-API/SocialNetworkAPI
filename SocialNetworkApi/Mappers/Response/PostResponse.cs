using SocialNetwork.Domain;
using System;

namespace SocialNetwork.Mappers.Responses
{
    public class PostResponse
    {
        public Guid Id { get; set; }
        public required string Content { get; set; }
        public required string Image { get; set; }
        public DateTime CreatedAt { get; set; }

        public static PostResponse FromDomain(Post post)
        {
            return new PostResponse
            {
                Id = post.Id,
                Content = post.Content,
                Image = post.Image,
                CreatedAt = post.CreatedAt
            };
        }
    }
}
