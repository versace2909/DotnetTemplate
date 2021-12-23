using System.Linq.Expressions;
using Application.DataAccess;
using Application.DTOs;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Core.Base;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.EFCore.DataAccess;

public class RepositoryBase<T, TKey> : IRepositoryBase<T, TKey> where T : BaseEntity<TKey>
{
    private readonly AppEfDbContext<TKey> _dbContext;
    private readonly DbSet<T> dbSet;
    private readonly IMapper _mapper;

    public RepositoryBase(AppEfDbContext<TKey> dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
        dbSet = _dbContext.Set<T>();
    }

    public async Task<IEnumerable<TResult>> GetAll<TResult>(Expression<Func<T, bool>>? where = null,
        Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null)
    {
        IQueryable<T> query = dbSet;
        if (where != null)
        {
            query = query.Where(where);
        }

        if (orderBy != null)
        {
            return await orderBy(query).ProjectTo<TResult>(_mapper.ConfigurationProvider).ToListAsync();
        }
        
        var result = await query.ProjectTo<TResult>(_mapper.ConfigurationProvider).ToListAsync();

        return result;
    }

    public async Task<TResponse> GetById<TResponse>(TKey id) where TResponse : BaseDTO<TKey>
    {
        var entity = await dbSet.ProjectTo<TResponse>(_mapper.ConfigurationProvider)
            .FirstOrDefaultAsync(x => x.Id.Equals(id));
        return entity;
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