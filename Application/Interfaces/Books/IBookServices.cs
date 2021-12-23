using Application.DTOs;
using Application.DTOs.Books;

namespace Application.Interfaces.Books;

public interface IBookServices
{
    Task<ResponseBaseModel<BookDTO<long>>> GetBookDetailAsync(long id);
    Task<ListResponseBaseModel<BookDTO<long>>> GetAllBooksAsync();
    Task<ResponseBaseModel<long>> InsertBookAsync(CreateBookDTO request);
    Task<ResponseBaseModel<bool>> UpdateBookById(BookDTO<long> request);
    Task<ResponseBaseModel<bool>> DeleteBookAsync(long id);
}