using System.Linq.Expressions;
using Application.DTOs;
using Core.Base;

namespace Application.DataAccess;

public interface IRepositoryBase<T, TKeyType> where T: BaseEntity<TKeyType>
{
    Task<IEnumerable<TResult>> GetAll<TResult>(Expression<Func<T,bool>> where = null,
        Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null);
    Task<TResponse> GetById<TResponse>(TKeyType id) where TResponse : BaseDTO<TKeyType>;
    Task<T> GetById(TKeyType id);
    void Insert(T entity);
    void Update(T entity);
    void Delete(T entity);
    void Delete(TKeyType id);
}