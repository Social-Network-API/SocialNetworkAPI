using SocialNetworkApi.Models;

namespace SocialNetworkApi.Services.Interface;

public interface IService<TBase, TResponse>
{ 
        Task<ServiceResult<TResponse>> CreateAsync(TBase entity);
        Task<ServiceResult<TResponse>> GetByIdAsync(Guid id);
        Task<ServiceResult<TResponse>> UpdateAsync(Guid id, TBase entity);
        Task<ServiceResult> DeleteAsync(Guid id);
}
