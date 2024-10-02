using SocialNetwork.Models;

namespace RestApi.Services.Interfaces
{
    public interface IService<TBase, TResponse>
    {
        Task<ServiceResult<TResponse>> CreateAsync(TBase entity);
        Task<ServiceResult<TResponse>> GetByIdAsync(Guid id);
        Task<ServiceResult<TResponse>> UpdateAsync(Guid id, TBase entity);
        Task<ServiceResult> DeleteAsync(Guid id);
    }
}