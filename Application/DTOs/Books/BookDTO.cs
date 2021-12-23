namespace Application.DTOs.Books;

public class BookDTO<Tkey> : BaseDTO<Tkey>
{
    public string BookName { get; set; }
    public int TotalPage { get; set; }
    public string Author { get; set; }
    public int BookType { get; set; }
}