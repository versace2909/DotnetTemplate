using Core.Base;
using Core.Shared.Enums;

namespace Core.Books;

public class Book<T> : BaseEntity<T>
{
    public string BookName { get; set; }
    public int TotalPage { get; set; }
    public string Author { get; set; }
    public int BookType { get; set; }
}