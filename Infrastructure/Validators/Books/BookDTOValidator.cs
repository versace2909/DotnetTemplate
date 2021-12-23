using Application.DTOs.Books;
using FluentValidation;

namespace Infrastructure.Validators.Books;

public class BookDTOValidator :  AbstractValidator<BookDTO>
{
    public BookDTOValidator()
    {
        RuleFor(x => x.BookName).NotEmpty().MaximumLength(250);
        RuleFor(x => x.TotalPage).NotEmpty().GreaterThan(0);
        RuleFor(x => x.Author).NotEmpty().MaximumLength(250);
        RuleFor(x => x.BookType).NotEmpty();
    }
}