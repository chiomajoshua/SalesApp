using Microsoft.EntityFrameworkCore;
using OrderManagement.Data.Entities;
using System.Linq.Expressions;

namespace OrderManagement.Data.Extensions;

public static class IQueryableExtensions
{
    public static IQueryable<T> ExtendWhere<T>(this IQueryable<T> query, Expression<Func<T, bool>> filter)
        => filter == null ? query : query.Where(filter);

    public static IQueryable<T> ExtendIncludes<T>(this IQueryable<T> query, Func<IQueryable<T>, IQueryable<T>> includeFunc = null)
         => includeFunc == null ? query : includeFunc(query);

    public static IQueryable<T> ExtendOrderBy<T>(this IQueryable<T> query, Expression<Func<T, object>> orderBy, bool ascending = false)
    {
        if (ascending)
        {
            return orderBy == null ? query : (query.OrderBy(orderBy));
        }
        else
        {
            return orderBy == null ? query : (query.OrderByDescending(orderBy));
        }
    }

    public static IQueryable<OrderHeader> ExtendOrderHeaderIncludes(this IQueryable<OrderHeader> query)
    {
        return query
                 .Include(o => o.OrderLine)
                 .Include(o => o.User);
    }
}