using SocialNetwork.Domain;
using SocialNetwork.Persistence.Repositories;
using SocialNetwork.Mappers.Responses;
using SocialNetwork.Models;
using RestApi.Services.Interfaces;

namespace SocialNetwork.Services
{
    public class CommentsService : IService<Comment, CommentResponse>
    {
        private readonly CommentsRepository _commentsRepository;

        public CommentsService(CommentsRepository commentsRepository)
        {
            _commentsRepository = commentsRepository;
        }

        public async Task<ServiceResult<CommentResponse>> CreateAsync(Comment comment)
        {
            var createdComment = await _commentsRepository.CreateAsync(comment);
            var response = CommentResponse.FromDomain(createdComment);
            return new ServiceResult<CommentResponse> { Data = response, Success = true };
        }

        public async Task<ServiceResult<CommentResponse>> GetByIdAsync(Guid commentId)
        {
            var comment = await _commentsRepository.GetByIdAsync(commentId);
            return comment is null
                ? new ServiceResult<CommentResponse> { Success = false }
                : new ServiceResult<CommentResponse> { Data = CommentResponse.FromDomain(comment), Success = true };
        }

        public async Task<ServiceResult<IEnumerable<CommentResponse>>> GetAllByPostIdAsync(Guid postId)
        {
            var comments = await _commentsRepository.GetByPostIdAsync(postId);
            var response = comments.Select(CommentResponse.FromDomain);
            return new ServiceResult<IEnumerable<CommentResponse>> { Data = response, Success = true };
        }

        public async Task<ServiceResult<CommentResponse>> UpdateAsync(Guid commentId, Comment updatedComment)
        {
            var existingComment = await _commentsRepository.UpdateAsync(commentId, updatedComment);
            if (existingComment == null)
                return new ServiceResult<CommentResponse> { Success = false };

            var response = CommentResponse.FromDomain(existingComment);
            return new ServiceResult<CommentResponse> { Data = response, Success = true };
        }

        public async Task<ServiceResult> DeleteAsync(Guid commentId)
        {
            await _commentsRepository.DeleteAsync(commentId);
            return new ServiceResult { Success = true };
        }
    }
}
