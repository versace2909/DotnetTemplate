using Application.DataAccess;
using Application.DTOs;
using Application.DTOs.Books;
using Application.Interfaces.Books;
using AutoMapper;
using Core.Books;
using Core.Shared.Constants;

namespace Infrastructure.EFCore.Implementations;

public class BookService : IBookServices
{
    private readonly IUnitOfWork<long> _context;
    private readonly IMapper _mapper;

    public BookService(IUnitOfWork<long> context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<ResponseBaseModel<BookDTO<long>>> GetBookDetailAsync(long id)
    {
        var result = await _context
            .BookRepository
            .GetById<BookDTO<long>>(id);
        return ResponseBaseModel<BookDTO<long>>.Succeed(result);
    }

    public async Task<ListResponseBaseModel<BookDTO<long>>> GetAllBooksAsync()
    {
        var result = await _context
            .BookRepository
            .GetAll<BookDTO<long>>();
        
        return ListResponseBaseModel<BookDTO<long>>.Succeed(result);
    }

    public async Task<ResponseBaseModel<long>> InsertBookAsync(CreateBookDTO request)
    {
        var newBook = _mapper.Map<Book<long>>(request);
        _context.BookRepository.Insert(newBook);
        await _context.SaveAsync();
        return ResponseBaseModel<long>.Succeed((long) newBook.Id, Messages.InsertSuccessful);
    }

    public async Task<ResponseBaseModel<bool>> UpdateBookById(BookDTO<long> request)
    {
        var book = await _context.BookRepository.GetById(request.Id);
        book.Author = request.Author;
        book.BookName = request.BookName;
        book.BookType = request.BookType;
        book.TotalPage = request.TotalPage;
        _context.BookRepository.Update(book);
        await _context.SaveAsync();
        return ResponseBaseModel<bool>.Succeed(true, Messages.UpdateSuccessful);
    }

    public async Task<ResponseBaseModel<bool>> DeleteBookAsync(long id)
    {
        var book = await _context.BookRepository.GetById(id);
        _context.BookRepository.Delete(book);
        await _context.SaveAsync();
        return ResponseBaseModel<bool>.Succeed(true, Messages.DeleteSuccessful);
    }
}