namespace Application.DTOs.Books;

public class BookDTO
{
    public long? Id { get; set; }
    public string BookName { get; set; }
    public int TotalPage { get; set; }
    public string Author { get; set; }
    public int BookType { get; set; }
}