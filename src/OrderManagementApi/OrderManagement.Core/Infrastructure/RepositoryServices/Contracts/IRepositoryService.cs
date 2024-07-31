using System.Linq.Expressions;

namespace OrderManagement.Core.Infrastructure.RepositoryServices.Contracts;

public interface IRepositoryService<TEntity>
    where TEntity : class
{
    Task<List<TEntity>> GetAsync(
                            Expression<Func<TEntity, bool>> filter,
                            Func<IQueryable<TEntity>, IQueryable<TEntity>>? includeFunc = null,
                            Expression<Func<TEntity, object>>? orderBy = null);

    Task<TEntity> GetSingleAsync(Expression<Func<TEntity, bool>> filter, bool trackChanges = false,
                                 Func<IQueryable<TEntity>, IQueryable<TEntity>>? includeFunc = null);

    Task<TEntity> InsertAsync(TEntity model, CancellationToken token = default);

    Task<bool> InsertBulkAsync(List<TEntity> model, CancellationToken token = default);

    Task<bool> UpdateAsync(TEntity model, CancellationToken token = default);
}