using Application.DTOs.Books;
using AutoMapper;
using Core.Books;

namespace Infrastructure.Mapper;

public class BookMappingProfile : Profile
{
    public BookMappingProfile()
    {
        CreateMap<Book, BookDTO>().ReverseMap();
        CreateMap<CreateBookDTO, Book>();
    }
}