using Microsoft.EntityFrameworkCore;
using OrderManagement.Core.Infrastructure.RepositoryServices.Contracts;
using OrderManagement.Data.DatabaseContext;
using System.Linq.Expressions;
using OrderManagement.Data.Extensions;

namespace OrderManagement.Core.Infrastructure.RepositoryServices.Implementation;

public class RepositoryService<TEntity> : IRepositoryService<TEntity>
    where TEntity : class
{
    private readonly OrderManagementDbContext _dbContext;

    public RepositoryService(OrderManagementDbContext dbContext) => _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));

    public async Task<List<TEntity>> GetAsync(Expression<Func<TEntity, bool>> filter, Func<IQueryable<TEntity>, IQueryable<TEntity>> includeFunc = null, Expression<Func<TEntity, object>> orderBy = null)
        => await QueryableBase.ExtendIncludes(includeFunc).ExtendWhere(filter).ExtendOrderBy(orderBy).ToListAsync();

    public async Task<TEntity> GetSingleAsync(Expression<Func<TEntity, bool>> filter, bool trackChanges = false, Func<IQueryable<TEntity>, IQueryable<TEntity>> includeFunc = null)
    {
        if (!trackChanges)
            return await _dbContext.Set<TEntity>().AsNoTracking().ExtendIncludes(includeFunc).ExtendWhere(filter).FirstOrDefaultAsync();
        return await QueryableBase.ExtendIncludes(includeFunc).ExtendWhere(filter).FirstOrDefaultAsync();
    }

    public async Task<TEntity> InsertAsync(TEntity model, CancellationToken token = default)
    {
        ArgumentNullException.ThrowIfNull(model);
        await _dbContext.Set<TEntity>().AddAsync(model, token);
        await _dbContext.SaveChangesAsync(token);
        return model;
    }

    public async Task<bool> InsertBulkAsync(List<TEntity> model, CancellationToken token = default)
    {
        if (model is null)
            throw new ArgumentNullException(nameof(model));

        await _dbContext.Set<List<TEntity>>().AddRangeAsync(model);
        return await _dbContext.SaveChangesAsync(token) > 0;
    }

    public async Task<bool> UpdateAsync(TEntity model, CancellationToken token = default)
    {
        ArgumentNullException.ThrowIfNull(model);
        _dbContext.Set<TEntity>().Update(model);
        return await _dbContext.SaveChangesAsync(token) > 0;
    }

    private IQueryable<TEntity> QueryableBase => _dbContext.Set<TEntity>().AsNoTracking().AsQueryable();
}