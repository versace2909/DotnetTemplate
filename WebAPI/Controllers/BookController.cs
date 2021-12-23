using Application.DTOs.Books;
using Application.Interfaces.Books;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[Route("/api/books")]
public class BookController : Controller
{
    private readonly IBookServices _bookServices;

    public BookController(IBookServices bookServices)
    {
        _bookServices = bookServices;
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetBook([FromQuery] long id)
    {
        var result = await _bookServices.GetBookDetailAsync(id);
        return new JsonResult(result);
    }

    [HttpGet("")]
    public async Task<IActionResult> GetAllBooks()
    {
        var result = await _bookServices.GetAllBooksAsync();
        return new JsonResult(result);
    }

    [HttpPost("")]
    public async Task<IActionResult> InsertBook([FromBody] CreateBookDTO request)
    {
        var result = await _bookServices.InsertBookAsync(request);
        return new JsonResult(result);
    }

    [HttpPut("")]
    public async Task<IActionResult> UpdateBook([FromBody] BookDTO request)
    {
        var result = await _bookServices.UpdateBookById(request);
        return new JsonResult(result);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteBook([FromQuery] long id)
    {
        var result = await _bookServices.DeleteBookAsync(id);
        return new JsonResult(result);
    }
}