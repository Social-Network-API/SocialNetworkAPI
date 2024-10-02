using SocialNetwork.Models;

namespace RestApi.Services.interfaces;

public interface IService<TBase, TResponse>
{
    Task<ServiceResult<TResponse>> CreateAsync(TBase entity);

    Task<ServiceResult<TResponse>> GetByIdAsync(Guid id);
}