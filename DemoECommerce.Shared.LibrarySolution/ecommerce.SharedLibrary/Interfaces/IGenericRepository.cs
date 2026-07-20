using ecommerce.SharedLibrary.Responses;
using System.Linq.Expressions;

namespace ecommerce.SharedLibrary.Interfaces
{
    public interface IGenericRepository<TEntity, TKey>
    where TEntity : class
    {
        Task<Response> CreateAsync(TEntity entity);
        Task<Response> UpdateAsync(TEntity entity);
        Task<Response> DeleteAsync(TKey id);
        Task<TEntity?> FindByIdAsync(TKey id);
        Task<TEntity?> GetByAsync(Expression<Func<TEntity,bool>> predicate);
        Task<IEnumerable<TEntity>> GetAllAsync();
        
    }
}
