using SocialNetwork.Entities;
using SocialNetwork.Mappers.Responses;
using SocialNetwork.Persistence.Repositories;
using SocialNetwork.Models;
using RestApi.Services.Interfaces;
using Microsoft.AspNetCore.Http.HttpResults;

namespace SocialNetwork.Services;
public class LikeService : IService<Like, LikeResponse>
{
    private readonly LikeRepository _likeRepository;

    public LikeService(LikeRepository likeRepository)
    {
        _likeRepository = likeRepository;
    }

    public async Task<ServiceResult<LikeResponse>> CreateAsync(Like like)
    {
        var createdLike = await _likeRepository.CreateAsync(like);
        var response = LikeResponse.FromDomain(createdLike);
        return new ServiceResult<LikeResponse> { Data = response, Success = true };
    }

    public async Task<ServiceResult<LikeResponse>> GetByIdAsync(Guid likeId)
    {
        return await Task.FromResult(new ServiceResult<LikeResponse> { Success = true });
    }

    public async Task<ServiceResult> DeleteAsync(Guid postId, Guid userId)
    {
        await _likeRepository.DeleteAsync(postId, userId);
        return new ServiceResult { Success = true };
    }

    public async Task<ServiceResult> DeleteAsync(Guid id)
    {
        return await Task.FromResult(new ServiceResult { Success = true });
    }

    public async Task<ServiceResult<LikeResponse>> UpdateAsync(Guid likeId, Like like)
    {
        return await Task.FromResult(new ServiceResult<LikeResponse> { Success = true });
    }

    public async Task<ServiceResult<IEnumerable<LikeResponse>>> GetLikesByPostIdAsync(Guid postId)
    {
        var likes = await _likeRepository.GetLikeByPostIdAsync(postId);
        var response = likes.Select(LikeResponse.FromDomain);
        return new ServiceResult<IEnumerable<LikeResponse>> { Data = response, Success = true };
    }

    public async Task<ServiceResult<IEnumerable<LikeResponse>>> GetLikesByUserIdAsync(Guid userId)
    {
        var likes = await _likeRepository.GetUserLikesAsync(userId);
        var response = likes.Select(LikeResponse.FromDomain);
        return new ServiceResult<IEnumerable<LikeResponse>> { Data = response, Success = true };
    }
}
