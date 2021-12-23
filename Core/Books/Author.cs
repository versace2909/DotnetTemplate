using Core.Base;

namespace Core.Books;

public class Author<TKey> : BaseEntity<TKey>
{
    public string AuthorName { get; set; }
}