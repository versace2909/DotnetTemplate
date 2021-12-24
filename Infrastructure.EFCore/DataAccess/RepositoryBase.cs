using System.Linq.Expressions;
using Application.DataAccess;
using AutoMapper;
using Core.Base;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.EFCore.DataAccess;

public class RepositoryBase<T, TKey> : IRepositoryBase<T, TKey> where T : BaseEntity<TKey>
{
    private readonly AppEfDbContext<TKey> _dbContext;
    private readonly DbSet<T> dbSet;

    public RepositoryBase(AppEfDbContext<TKey> dbContext)
    {
        _dbContext = dbContext;
        dbSet = _dbContext.Set<T>();
    }

    public async Task<IEnumerable<T>> GetAll(Expression<Func<T, bool>>? where = null,
        Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null)
    {
        IQueryable<T> query = dbSet;
        if (where != null)
        {
            query = query.Where(where);
        }

        if (orderBy != null)
        {
            return await orderBy(query).ToListAsync();
        }
        
        var result = await query.ToListAsync();

        return result;
    }

    public async Task<T> GetById(TKey id)
    {
        var entity = await dbSet
            .FirstOrDefaultAsync(x => x.Id.Equals(id));
        return entity;
    }

    public void Insert(T entity)
    {
        dbSet.Add(entity);
    }

    public void Update(T entity)
    {
        dbSet.Attach(entity);
        _dbContext.Entry(entity).State = EntityState.Modified;
    }

    public void Delete(T entity)
    {
        _dbContext.Entry(entity).State = EntityState.Detached;
        dbSet.Remove(entity);
    }

    public void Delete(TKey id)
    {
        var deletedEntity = dbSet.Find(id);
        Delete(deletedEntity);
    }
}