using System.Linq.Expressions;

namespace FintechWallet.Repositories
{
    public interface IRepository<TEntity> where TEntity : class
    {
        Task AddAsync(TEntity entity);
        Task UpdateAsync(TEntity entity);
        Task DeleteAsync(TEntity entity);
        Task<TEntity?> GetByIdAsync(object id);
        Task<IEnumerable<TEntity>> GetAllAsync();
        Task<TEntity?> FindByConditionAsync(Expression<Func<TEntity, bool>> predicate);
        Task<IEnumerable<TEntity>> FindItemsByConditionAsync(Expression<Func<TEntity, bool>> predicate);
        Task SaveChangesAsync();
    }
}
