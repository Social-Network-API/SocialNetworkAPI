using SocialNetworkApi.DataAccess.Entities;
using SocialNetworkApi.DataAccess.Repositories.Concretes;
using SocialNetworkApi.Mappers.Response;
using SocialNetworkApi.Models;
using SocialNetworkApi.Services.Interface;

namespace SocialNetworkApi.Services;
public class PostsService : IService<Post, PostResponse>
{
    private readonly PostsRepository _postsRepository;
    private readonly FriendsRepository _friendsRepository;
    private readonly CommentsRepository _commentsRepository;

    public PostsService(PostsRepository postsRepository, FriendsRepository friendsRepository, CommentsRepository commentsRepository)
    {
        _postsRepository = postsRepository;
        _friendsRepository = friendsRepository;
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
            .Where(p => p.UserId == userId || _friendsRepository.AreFriendsAsync(userId, p.UserId).Result)
            .OrderByDescending(p => p.CreatedAt);

        var response = friendPosts.Select(PostResponse.FromDomain);
        return new ServiceResult<IEnumerable<PostResponse>> { Data = response, Success = true };
    }
    public async Task<ServiceResult<IEnumerable<PostWithCommentsResponse>>> GetHomePostsWithCommentsAsync(Guid userId)
    {
        var posts = await _postsRepository.GetHomePostsAsync(userId);

        var postsWithComments = new List<PostWithCommentsResponse>();

        foreach (var post in posts)
        {
            var comments = await _commentsRepository.GetCommentsForPostAsync(post.Id);
            var postWithComments = PostWithCommentsResponse.FromDomain(post, comments);
            postsWithComments.Add(postWithComments);
        }

        return new ServiceResult<IEnumerable<PostWithCommentsResponse>> { Data = postsWithComments, Success = true };
    }
}

