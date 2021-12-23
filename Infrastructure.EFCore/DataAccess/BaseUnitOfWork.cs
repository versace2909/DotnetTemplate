using Application.DataAccess;
using AutoMapper;
using Core.Books;

namespace Infrastructure.EFCore.DataAccess;

public class BaseUnitOfWork<TKey> : IUnitOfWork<TKey>
{
    private AppEfDbContext<TKey> _dbContext;
    private IMapper _mapper;

    public BaseUnitOfWork(AppEfDbContext<TKey> dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }

    public IRepositoryBase<Book<TKey>, TKey> BookRepository =>
        new RepositoryBase<Book<TKey>, TKey>(_dbContext, _mapper);

    public async Task<bool> SaveAsync()
    {
        var result = await _dbContext.SaveChangesAsync();
        return result > 0;
    }

    public bool Save()
    {
        var result = _dbContext.SaveChanges();
        return result > 0;
    }

    private bool disposed = false;

    protected virtual void Dispose(bool disposing)
    {
        if (!this.disposed)
        {
            if (disposing)
            {
                _dbContext.Dispose();
            }
        }

        this.disposed = true;
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }
}