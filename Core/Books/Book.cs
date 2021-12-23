using Core.Base;
using Core.Shared.Enums;

namespace Core.Books;

public class Book : BaseEntity
{
    public string BookName { get; set; }
    public int TotalPage { get; set; }
    public string Author { get; set; }
    public int BookType { get; set; }
}