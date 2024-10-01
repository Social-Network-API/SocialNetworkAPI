namespace SocialNetworkApi.DataAccess.Repositories.Interfaces;
public interface IBaseRepository<T>
{
    Task<T> CreateAsync(T entity);
    Task<IList<T>> GetAll();
    Task<T> GetById(Guid id);
    Task<bool> UpdateAsync(Guid id, T updatedExpense);
    Task<bool> DeleteAsync(Guid id);
}
