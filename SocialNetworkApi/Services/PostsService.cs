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
    private readonly LikeRepository _likeRepository;
    private readonly UserRepository _userRepository;

    public PostsService(PostsRepository postsRepository, FollowRepository followRepository, CommentsRepository commentsRepository ,LikeRepository likeRepository , UserRepository userRepository)
    {
        _postsRepository = postsRepository;
        _followRepository = followRepository;
        _commentsRepository = commentsRepository;
        _likeRepository = likeRepository;
        _userRepository = userRepository;

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
    
    public async Task<ServiceResult<IEnumerable<PostWithDetailsResponse>>> GetHomePostsWithCommentsAsync(Guid userId)
    {
        var posts = await _postsRepository.GetHomePostsAsync(userId);
        var postIds = posts.Select(p => p.Id).ToList();
        var comments = await _commentsRepository.GetCommentsForPostsAsync(postIds);
        var likes = await _likeRepository.GetLikesForPostsAsync(postIds);
        var commentsGrouped = comments.GroupBy(c => c.PostId)
            .ToDictionary(g => g.Key, g => g.ToList());
        var likesGrouped = likes.GroupBy(l => l.PostId)
            .ToDictionary(g => g.Key, g => g.ToList());
        var postsWithDetails = posts.Select(post =>
        {
            var user = _userRepository.GetByIdAsync(post.UserId).Result;
            var commentsForPost = commentsGrouped.ContainsKey(post.Id) ? commentsGrouped[post.Id] : new List<Comment>();
            var likesForPost = likesGrouped.ContainsKey(post.Id) ? likesGrouped[post.Id] : new List<Like>();

            return PostWithDetailsResponse.FromDomain(post, user, commentsForPost, likesForPost);
        }).ToList();

        return new ServiceResult<IEnumerable<PostWithDetailsResponse>> { Data = postsWithDetails, Success = true };
    }
    
    public async Task<ServiceResult<IEnumerable<PostWithDetailsResponse>>> GetPostsLikedByUserAsync(Guid userId)
    {
        var likedPosts = await _likeRepository.GetPostsLikedByUserAsync(userId);
        var postIds = likedPosts.Select(p => p.Id).ToList();
        var comments = await _commentsRepository.GetCommentsForPostsAsync(postIds);
        var likes = await _likeRepository.GetLikesForPostsAsync(postIds);
        var commentsGrouped = comments.GroupBy(c => c.PostId)
            .ToDictionary(g => g.Key, g => g.ToList());
        var likesGrouped = likes.GroupBy(l => l.PostId)
            .ToDictionary(g => g.Key, g => g.ToList());

        var postsWithDetails = likedPosts.Select(post =>
        {
            var user = _userRepository.GetByIdAsync(post.UserId).Result;
            var commentsForPost = commentsGrouped.ContainsKey(post.Id) ? commentsGrouped[post.Id] : new List<Comment>();
            var likesForPost = likesGrouped.ContainsKey(post.Id) ? likesGrouped[post.Id] : new List<Like>();

            return PostWithDetailsResponse.FromDomain(post, user, commentsForPost, likesForPost);
        }).ToList();

        return new ServiceResult<IEnumerable<PostWithDetailsResponse>> { Data = postsWithDetails, Success = true };
    }

}


