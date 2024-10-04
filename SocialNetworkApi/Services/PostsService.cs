using SocialNetworkApi.Business.Mappers.Response;
using SocialNetworkApi.DataAccess.Entities;
using SocialNetworkApi.DataAccess.Repositories.Concretes;
using SocialNetworkApi.Models;
using SocialNetworkApi.Services.Interface;

namespace SocialNetworkApi.Services;
public class PostsService : IService<Post, PostResponse>
{
    private readonly PostsRepository _postsRepository;
    private readonly FollowRepository _followRepository;
    private readonly CommentsRepository _commentsRepository;

    public PostsService(PostsRepository postsRepository, FollowRepository followRepository, CommentsRepository commentsRepository)
    {
        _postsRepository = postsRepository;
        _followRepository = followRepository;
        _commentsRepository = commentsRepository;
    }

    public async Task<ServiceResult<PostResponse>> CreateAsync(Post post)
    {
        await _postsRepository.CreateAsync(post);
        var response = PostResponse.FromDomain(post);
        return new ServiceResult<PostResponse> { Data = response, Success = true };
    }

    public async Task<ServiceResult<PostResponse>> GetByIdAsync(Guid postId)
    {
        var post = await _postsRepository.GetByIdAsync(postId);
        return post is null
            ? new ServiceResult<PostResponse> { Data = null, Success = false }
            : new ServiceResult<PostResponse> { Data = PostResponse.FromDomain(post), Success = true };
    }

    public async Task<ServiceResult<PostResponse>> UpdateAsync(Guid postId, Post updatedPost)
    {
        var existingPost = await _postsRepository.GetByIdAsync(postId);

        if (existingPost == null)
        {
            return new ServiceResult<PostResponse> { Success = false };
        }

        existingPost.Content = updatedPost.Content;
        existingPost.ImageUrl = updatedPost.ImageUrl;

        await _postsRepository.EditAsync(postId, existingPost);

        var response = PostResponse.FromDomain(existingPost);
        return new ServiceResult<PostResponse> { Data = response, Success = true };
    }

    public async Task<ServiceResult> DeleteAsync(Guid postId)
    {
        await _postsRepository.DeleteAsync(postId);
        return new ServiceResult { Success = true };
    }

    public async Task<ServiceResult<IEnumerable<PostResponse>>> GetUserPostsAsync(Guid userId)
    {
        var posts = await _postsRepository.GetUserPostsAsync(userId);
        var response = posts.Select(PostResponse.FromDomain);
        return new ServiceResult<IEnumerable<PostResponse>> { Data = response, Success = true };
    }

    public async Task<ServiceResult<IEnumerable<PostResponse>>> GetHomePostsAsync(Guid userId)
    {
        var posts = await _postsRepository.GetHomePostsAsync(userId);
        var friendPosts = posts
            .Where(p => p.UserId == userId || _followRepository.AreFriendsAsync(userId, p.UserId).Result)
            .OrderByDescending(p => p.CreatedAt);

        var response = friendPosts.Select(PostResponse.FromDomain);
        return new ServiceResult<IEnumerable<PostResponse>> { Data = response, Success = true };
    }
    
    public async Task<ServiceResult<IEnumerable<PostWithCommentsResponse>>> GetHomePostsWithCommentsAsync(Guid userId)
    {
        var posts = await _postsRepository.GetHomePostsAsync(userId);
    
        var postIds = posts.Select(p => p.Id).ToList();
        var comments = await _commentsRepository.GetCommentsForPostsAsync(postIds);
    
        var commentsGrouped = comments.GroupBy(c => c.PostId)
            .ToDictionary(g => g.Key, g => g.ToList());

        var postsWithComments = posts.Select(post => 
        {
            var commentsForPost = commentsGrouped.ContainsKey(post.Id) ? commentsGrouped[post.Id] : new List<Comment>();
            return PostWithCommentsResponse.FromDomain(post, commentsForPost);
        }).ToList();

        return new ServiceResult<IEnumerable<PostWithCommentsResponse>> { Data = postsWithComments, Success = true };
    }

}


