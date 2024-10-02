using SocialNetwork.Entities;

namespace SocialNetwork.Mappers.Requests
{
    public record CreateCommentRequest(Guid UserId, string Content)
    {
        public Comment ToDomain(Guid postId)
        {
            return new Comment
            {
                PostId = postId,
                UserId = UserId,
                Content = Content
            };
        }
    }
}