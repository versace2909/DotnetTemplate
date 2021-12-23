using Application.DTOs;
using Application.DTOs.Books;
using Application.Interfaces.Books;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Core.Books;
using Core.Shared.Constants;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Implementations;

public class BookServices : IBookServices
{
    private readonly AppDbContext _dbContext;
    private readonly IMapper _mapper;

    public BookServices(AppDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }

    public async Task<ResponseBaseModel<BookDTO>> GetBookDetailAsync(long id)
    {
        var result = await _dbContext
            .Books
            .AsNoTracking()
            .ProjectTo<BookDTO>(_mapper.ConfigurationProvider)
            .FirstOrDefaultAsync(x => x.Id == id);
        return ResponseBaseModel<BookDTO>.Succeed(result);
    }

    public async Task<ListResponseBaseModel<BookDTO>> GetAllBooksAsync()
    {
        var results = await _dbContext
            .Books
            .AsNoTracking()
            .ProjectTo<BookDTO>(_mapper.ConfigurationProvider)
            .ToListAsync();
        return ListResponseBaseModel<BookDTO>.Succeed(results);
    }

    public async Task<ResponseBaseModel<long>> InsertBookAsync(CreateBookDTO request)
    {
        var newBook = _mapper.Map<Book>(request);
        _dbContext.Books.Add(newBook);
        await _dbContext.SaveChangesAsync();
        return ResponseBaseModel<long>.Succeed(newBook.Id, Messages.InsertSuccessful);
    }

    public async Task<ResponseBaseModel<bool>> UpdateBookById(BookDTO request)
    {
        var book = await _dbContext.Books.FirstOrDefaultAsync(x => x.Id == request.Id);
        book.Author = request.Author;
        book.BookName = request.BookName;
        book.BookType = request.BookType;
        book.TotalPage = request.TotalPage;
        await _dbContext.SaveChangesAsync();
        return ResponseBaseModel<bool>.Succeed(true, Messages.UpdateSuccessful);
    }

    public async Task<ResponseBaseModel<bool>> DeleteBookAsync(long id)
    {
        var book = await _dbContext.Books.FirstOrDefaultAsync(x => x.Id == id);
        book.Deleted = true;
        book.DeletedBy = "ADMIN";
        book.DeletedDate = DateTime.UtcNow;
        await _dbContext.SaveChangesAsync();
        return ResponseBaseModel<bool>.Succeed(true, Messages.DeleteSuccessful);

    }
}