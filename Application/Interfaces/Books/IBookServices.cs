using Application.DTOs;
using Application.DTOs.Books;

namespace Application.Interfaces.Books;

public interface IBookServices
{
    Task<ResponseBaseModel<BookDTO>> GetBookDetailAsync(long id);
    Task<ListResponseBaseModel<BookDTO>> GetAllBooksAsync();
    Task<ResponseBaseModel<long>> InsertBookAsync(CreateBookDTO request);
    Task<ResponseBaseModel<bool>> UpdateBookById(BookDTO request);
    Task<ResponseBaseModel<bool>> DeleteBookAsync(long id);
}