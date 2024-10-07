using SocialNetworkApi.DataAccess.Entities;

namespace SocialNetworkApi.Business.Mappers.Response;
public record PostWithDetailsResponse(
    Guid PostId,
    UserResponse User,
    string Content,
    string ImageUrl,
    DateTime CreatedAt,
    CommentSummary Comments,
    LikeSummary Likes)
{
    public static PostWithDetailsResponse FromDomain(Post post, User user, IEnumerable<Comment> comments, IEnumerable<Like> likes)
    {
        var userResponse = UserResponse.FromDomain(user);

        var commentList = comments.Select(c => new CommentResponse(
            c.Id,
            c.PostId,
            c.UserId,
            c.Content,
            c.CreatedAt
        )).ToList();

        var likeUsers = likes.Take(2).Select(l => new UserResponse(
            l.UserId,
            user.Name, 
            user.Email, 
            user.CreatedAt,
            user.ProfilePicture
        )).ToList();

        return new PostWithDetailsResponse(
            post.Id,
            userResponse,
            post.Content,
            post.ImageUrl,
            post.CreatedAt,
            new CommentSummary(commentList.Count, commentList),
            new LikeSummary(likes.Count(), likeUsers)
        );
    }
}

public record CommentSummary(int Total, IEnumerable<CommentResponse> List);
public record LikeSummary(int Total, IEnumerable<UserResponse> UsersPreview);
