using RestApi.Services.Interfaces;
using SocialNetwork.Domain;
using SocialNetwork.Mappers.Responses;
using SocialNetwork.Persistence.Repositories;
using SocialNetwork.Models;


namespace SocialNetwork.Services
{
    public class PostsService : IService<Post, PostResponse>
    {
        private readonly PostsRepository _postsRepository;

        public PostsService(PostsRepository postsRepository)
        {
            _postsRepository = postsRepository;
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
            var posts = await _postsRepository.GetHomePostsAsync( userId);
            var response = posts.Select(PostResponse.FromDomain);
            return new ServiceResult<IEnumerable<PostResponse>> { Data = response, Success = true };
        }
    }
}
