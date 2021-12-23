using Core.Books;

namespace Application.DataAccess;

public interface IUnitOfWork<TKey> : IDisposable
{
    IRepositoryBase<Book<TKey>, TKey>  BookRepository { get; }
    Task<bool> SaveAsync();
    bool Save();
}